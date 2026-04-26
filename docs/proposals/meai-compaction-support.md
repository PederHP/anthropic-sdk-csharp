# Idiomatic MEAI support for the Beta compaction feature

## Background

The Anthropic API now exposes server-side context compaction
(<https://platform.claude.com/docs/en/build-with-claude/compaction>). The C#
SDK already has full schema-level support for it under
`Anthropic.Models.Beta.Messages` (`BetaContextManagementConfig`,
`BetaCompact20260112Edit`, `BetaCompactionBlock` /
`BetaCompactionBlockParam`, `BetaCompactionContentBlockDelta`,
`BetaCompactionIterationUsage`, `BetaIterationsUsageItems`, etc.), and these
types are wired into the beta `MessageCreateParams.ContextManagement`,
`MessageCountTokensParams.ContextManagement`, `BatchCreateParams`,
`BetaMessage.ContextManagement`, `BetaUsage.Iterations`, and the streaming
aggregator.

The Microsoft.Extensions.AI bridge — `AnthropicClientExtensions.cs` and
`Services/Beta/Messages/AnthropicBetaClientExtensions.cs` — has **no**
references to compaction or context management. A user who calls
`client.Beta.AsIChatClient(...)` today can technically opt into compaction,
but only by hand-rolling a `RawRepresentationFactory`, fishing the
`BetaMessage` out of `ChatResponse.RawRepresentation`, summing usage
iterations themselves, and manually converting each returned
`BetaCompactionBlock` to a `BetaCompactionBlockParam` for the next turn.
None of this is discoverable from `IChatClient` / `ChatOptions` /
`AIContent`.

This document describes the work needed to make compaction a first-class
feature of the MEAI bridge.

## Goals

1. A user enabling compaction shouldn't have to know about
   `BetaContextManagementConfig` or `RawRepresentationFactory`.
2. Compaction blocks returned by the model surface as something more useful
   than an opaque `AIContent { RawRepresentation = ... }`.
3. Token accounting in `ChatResponse.Usage` reflects the documented billing
   rule (sum of all `iterations`, not just the final non-compaction
   iteration).
4. A user round-tripping a `ChatResponse` back into the next
   `GetResponseAsync` / `GetStreamingResponseAsync` call gets correct
   compaction-block continuation without writing any conversion code.
5. Streaming surfaces compaction events at parity with non-streaming.

Non-goals: introducing compaction support to the **non-beta** `AsIChatClient`
overload. Compaction is a beta feature; only the
`IBetaService.AsIChatClient` path needs to know about it.

## Scope of changes

All file paths are relative to the repo root. Line numbers are at the time of
writing and are intended only as anchors for reviewers.

### 1. Beta header enum

`src/Anthropic/Models/Beta/AnthropicBeta.cs`

Add the new beta-header value referenced by the docs page:

```csharp
Compact2026_01_12,            // "compact-2026-01-12"
```

with matching cases in `AnthropicBetaConverter.Read` / `Write`. The existing
`ContextManagement2025_06_27` value should stay; both headers are still
referenced by the platform during the migration window.

### 2. Strongly-typed `applied_edits` for compaction

`src/Anthropic/Models/Beta/Messages/BetaContextManagementResponse.cs`

`AppliedEdit` is currently a discriminated union over
`BetaClearToolUses20250919EditResponse` and
`BetaClearThinking20251015EditResponse`. The codegen for the
`compact_20260112` applied-edit variant is missing. Add a
`BetaCompact20260112EditResponse` model and extend the union (constructors,
`TryPickBetaCompact20260112EditResponse`, `Switch` / `Match` overloads,
implicit operator, `EditConverter` case). This is purely a schema gap; it
should arrive via the next codegen sync from the spec, but the proposal
should call it out so it isn't forgotten when wiring the MEAI helpers.

### 3. A `ChatOptions` helper to enable compaction

New file: `src/Anthropic/Services/Beta/Messages/CompactionChatOptionsExtensions.cs`
(or fold into `AnthropicBetaClientExtensions.cs` next to `AsAITool`).

Public surface:

```csharp
public static class CompactionChatOptionsExtensions
{
    /// <summary>
    /// Enables server-side context compaction for requests issued through
    /// the beta <see cref="IChatClient"/> returned by <c>AsIChatClient</c>.
    /// </summary>
    /// <param name="options">The chat options to mutate.</param>
    /// <param name="triggerInputTokens">
    /// Trigger threshold in input tokens. Defaults to 150,000; minimum 50,000
    /// per the API.
    /// </param>
    /// <param name="instructions">Optional custom summarization prompt.</param>
    /// <param name="pauseAfterCompaction">
    /// When true, the API returns with stop_reason=compaction after producing
    /// the summary instead of continuing the turn.
    /// </param>
    /// <returns>The same <paramref name="options"/> for chaining.</returns>
    public static ChatOptions EnableAnthropicCompaction(
        this ChatOptions options,
        int triggerInputTokens = 150_000,
        string? instructions = null,
        bool pauseAfterCompaction = false);
}
```

Implementation responsibilities:

- Stash a `BetaCompact20260112Edit` on a per-options "additional edits"
  collection that the chat client picks up in `GetMessageCreateParams`. The
  cleanest carrier is `ChatOptions.AdditionalProperties[KnownKey]` where
  `KnownKey` is a private constant; the chat client owns reading it back.
- Add the `compact-2026-01-12` beta header automatically in
  `GetMessageCreateParams` whenever the additional-edits collection contains
  a compaction edit (and the user hasn't already supplied it via
  `RawRepresentationFactory`).
- Compose with an existing user-supplied `RawRepresentationFactory`:
  if the factory returns a `MessageCreateParams` that already has
  `ContextManagement.Edits`, append rather than overwrite, and dedupe on
  edit `Type`.

A symmetrical `DisableAnthropicCompaction` is not necessary — users can omit
the call.

### 4. Output mapping — `BetaCompactionBlock` → `AIContent`

`src/Anthropic/Services/Beta/Messages/AnthropicBetaClientExtensions.cs`,
function `ContentBlockValueToAIContent` (around line 1477) and the streaming
`BetaRawContentBlockStartEvent` switch (around line 371).

Add a case for `BetaCompactionBlock`. Two reasonable shapes; recommend the
first:

a. Surface the summary as a `TextReasoningContent` with the raw block
   preserved on `RawRepresentation`. Rationale: the summary is the model's
   condensed memory of prior turns, conceptually closer to "thinking" than
   to assistant-visible text; using `TextReasoningContent` keeps it out of
   `Text` concatenation but still readable. Set `ProtectedData` to the
   block's `EncryptedContent` so round-trip preserves it verbatim.

b. A purpose-built `AnthropicCompactionContent : AIContent` type, exposed
   from the Anthropic-specific namespace. Use only if MEAI maintainers
   reject overloading `TextReasoningContent`.

Whichever shape is chosen, **do the same mapping in the streaming path**:
add a case for the start event of a `compaction` content block, and for the
`BetaCompactionContentBlockDelta` (`compaction_delta`) delta in the
content-block-delta switch. Per the docs the delta carries the full summary
in a single delta event, so the streaming case can finalize on the delta
without buffering across multiple deltas.

### 5. Round-tripping back into the next request

`AnthropicBetaClientExtensions.cs`, `CreateMessageParams` (the `foreach
content in message.Contents` switch around line 568).

The existing path
`case AIContent ac when ac.RawRepresentation is BetaContentBlockParam
rawContent` already handles arbitrary param-shape round-trips, but that only
fires when the user has constructed a `BetaContentBlockParam` themselves.
Add a case for the `AIContent` shape produced in step 4 — i.e. a
`TextReasoningContent` (or `AnthropicCompactionContent`) whose
`RawRepresentation` is a `BetaCompactionBlock` — and convert it on the fly
to `BetaCompactionBlockParam` (copying `Content` and `EncryptedContent`,
preserving any `cache_control` set via `AIContentCacheExtensions`).

### 6. `ToUsageDetails` — sum the iterations

`AnthropicBetaClientExtensions.cs`, lines 1393 and 1411.

Today `ToUsageDetails(BetaUsage)` reads only the top-level
`InputTokens` / `OutputTokens` / `CacheCreation*` / `CacheRead*`. The doc
explicitly warns:

> Top-level `input_tokens`/`output_tokens` reflect only non-compaction
> iterations. Sum all `iterations` entries for total token consumption and
> billing.

Update the helper so that, when `usage.Iterations` is non-null and
non-empty:

- `UsageDetails.InputTokenCount` = sum of `InputTokens` +
  `CacheCreationInputTokens` + `CacheReadInputTokens` across **all**
  iterations.
- `UsageDetails.OutputTokenCount` = sum of `OutputTokens` across all
  iterations.
- `UsageDetails.CachedInputTokenCount` = sum of `CacheReadInputTokens`
  across all iterations.
- `UsageDetails.AdditionalCounts` gets per-iteration breakdowns under keys
  like `"CompactionInputTokens"`, `"CompactionOutputTokens"`,
  `"MessageInputTokens"`, `"MessageOutputTokens"` so users who care can
  separate them.

When `Iterations` is null/empty, behaviour is unchanged.

The streaming path (`ToUsageDetails(BetaMessageDeltaUsage)`) does not need
the iteration sum — the delta event publishes only the cumulative
non-compaction usage anyway. Document that limitation in a comment near the
streaming handler so future readers know the non-streaming path is the
authoritative one for compaction-aware totals.

### 7. Surface `ContextManagement` response info

The model often returns `BetaMessage.ContextManagement` with an
`AppliedEdits` array describing what was actually compacted. Expose this on
the `ChatResponse`:

- Stash the response object (or, more conservatively, the `AppliedEdits`
  array) on `ChatResponse.AdditionalProperties` under a documented key (e.g.
  `"AnthropicAppliedContextEdits"`), or
- Provide a `ChatResponse.GetAnthropicAppliedContextEdits()` extension that
  reaches into `RawRepresentation` for callers who want the typed shape.

The second option keeps the extra payload off the hot path; the first is
more discoverable. Recommend providing the extension and documenting it in
the existing extensions doc comments.

### 8. Tests

`src/Anthropic.Tests/Services/Beta/`

- `BetaCompactionChatOptionsTest`: round-trips
  `EnableAnthropicCompaction` through `GetMessageCreateParams`,
  asserts the produced `ContextManagement.Edits` contains a single
  `BetaCompact20260112Edit` with the expected trigger / instructions /
  pause flag, and that the `Betas` list contains
  `compact-2026-01-12`.
- `BetaCompactionResponseMappingTest`: feeds a synthetic `BetaMessage`
  containing both a `BetaCompactionBlock` and a `BetaTextBlock` through the
  non-streaming response path and asserts the `ChatMessage.Contents`
  contains the summary surfaced as `TextReasoningContent` with the block
  preserved on `RawRepresentation`.
- `BetaCompactionStreamingTest`: same as above but for the streaming path,
  using a recorded SSE stream (or a synthetic
  `IAsyncEnumerable<BetaRawMessageStreamEvent>`).
- `BetaCompactionRoundTripTest`: builds a `ChatHistory` whose previous
  assistant turn contains the `TextReasoningContent` produced above, runs
  it back through `GetResponseAsync`, and asserts the outgoing
  `MessageCreateParams.Messages` contains a `BetaCompactionBlockParam`
  with the same `Content` and `EncryptedContent`.
- `BetaCompactionUsageTest`: feeds a `BetaUsage.Iterations` containing one
  `compaction` and one `message` iteration into `ToUsageDetails` and
  asserts `InputTokenCount` / `OutputTokenCount` reflect the sum.

### 9. Documentation

- A short section in `src/Anthropic/README.md` (or wherever the MEAI
  examples live) showing
  `client.Beta.AsIChatClient(...)` plus
  `options.EnableAnthropicCompaction(triggerInputTokens: 150_000)`,
  with a note pointing at the upstream docs page.
- A doc comment on `EnableAnthropicCompaction` explaining the minimum
  trigger (50,000 tokens), the supported models, and that the feature is
  ignored by non-Anthropic `IChatClient` implementations.

## Open questions

1. **`TextReasoningContent` vs new type.** Choosing
   `TextReasoningContent` keeps the surface minimal but conflates
   compaction summaries with extended-thinking content. A dedicated
   `AnthropicCompactionContent` is more explicit but adds a public type
   that has no MEAI-cross-provider analogue. Recommend
   `TextReasoningContent` and revisit if user feedback says the conflation
   bites.
2. **Billing in `UsageDetails`.** Should the top-level
   `InputTokenCount` reflect billable totals (sum of iterations) or only
   the message iteration's value? Recommend billable totals to match the
   doc's guidance, with the per-iteration breakdown in
   `AdditionalCounts` for users who need to separate.
3. **Beta header gate.** `EnableAnthropicCompaction` should add the beta
   header automatically. If the user has set `Betas` themselves on a
   `RawRepresentationFactory`-produced params, the helper should append
   without duplicating. Confirm this matches the pattern used by the
   skills helper today.
4. **`MessageCountTokens`.** The token-counting endpoint also accepts
   `context_management`; an analogous helper on the count-tokens path is
   probably worth providing once the chat-client path lands. Out of scope
   for the initial PR but worth a follow-up issue.

## Suggested PR breakdown

1. **PR 1 — schema gap.** `Compact2026_01_12` enum value and
   `BetaCompact20260112EditResponse` applied-edit variant. Pure codegen-ish
   change; no behaviour.
2. **PR 2 — output mapping + usage.** `BetaCompactionBlock` →
   `TextReasoningContent` in non-streaming and streaming paths; iteration
   summing in `ToUsageDetails`. Tests for both.
3. **PR 3 — round-trip + opt-in helper.** `EnableAnthropicCompaction`
   extension, round-trip handling in `CreateMessageParams`, README example,
   integration test.

Splitting like this keeps each PR reviewable and lets PR 1 land
independently if the schema variant arrives via codegen first.
