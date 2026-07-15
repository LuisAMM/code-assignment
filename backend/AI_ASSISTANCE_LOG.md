# AI Assistance Log — July 15, 2026

Log of AI-assisted work on the currency-to-words backend (multi-language support).

---

## 1. Design consultation: adding German support

**Prompt (paraphrased):** "I need this service to support German. Should I create another service just for German? Or a service just for transforming numbers to string, keeping decomposition/parsing here? Should the frontend pass the language as an enum?"

**Support given (advice only, no code):**
- Recommended a **strategy pattern** instead of duplicating the service: one language-agnostic orchestrator + one `INumberToWordsConverter` implementation per language.
- Explained why German is not just a word-table swap (units before tens: *dreiundzwanzig*; glued single words: *einhundertdreiundzwanzig*; *ein* vs *eins*; *eine Million* / *zwei Millionen* pluralization).
- Noted that decomposition (millions/thousands/hundreds/cents + range validation) is 100% language-independent and should stay in one place.
- Recommended passing the language as a **query parameter bound to an enum** (`?language=de`) with ISO 639-1 wire values, over the `Accept-Language` header (more explicit and testable for a user-selected language).

---

## 2. Clarification: DI selection & decomposition class design

**Prompt (paraphrased):** "How would I choose between en and de? Is it because they are not injected via DI? Also I'd like a non-DI class that decomposes the amount into millions/thousands/hundreds/cents — should the converters be DI? Help with naming. Don't implement anything."

**Support given (advice only, no code):**
- Explained that both converters **can** live in DI: register both against `INumberToWordsConverter`, inject `IEnumerable<INumberToWordsConverter>`, select at runtime via a `Language` discriminator property. Also mentioned factory and .NET 8 keyed-services alternatives.
- Endorsed a plain (non-DI) decomposition type; suggested a `readonly record struct` with a factory method.
- Naming options given: `DecomposedAmount` / `AmountDecomposer`, `CurrencyParts` / `CurrencyPartsParser`, `AmountBreakdown`. Recommended **`DecomposedAmount.From(decimal)`**.
- Recommended keeping converters in DI (extensibility: new language = new class + one registration line) and keeping range validation in the service (business rule, not math fact).

---

## 3. Implementation: refactoring to the strategy architecture

**Prompt (paraphrased):** "Let's go with the list of services registered with a language, selected at runtime. Create the DecomposedAmount class, move the logic out of the current service, implement the English converter with what we have, and leave a placeholder for the German one — I'll do that myself."

**Support given (code):**
- **New files:**
  - `Backend.Api/Currency/Domain/Language.cs` — `Language` enum (`En`, `De`).
  - `Backend.Api/Currency/Domain/DecomposedAmount.cs` — non-DI `readonly record struct` with `From(decimal)` factory and helpers (`IsZero`, `HasWholePart`, `WholePartIsOne`).
  - `Backend.Api/Currency/Service/Converters/INumberToWordsConverter.cs` — strategy interface (`Language` property + `ToWords(DecomposedAmount)`).
  - `Backend.Api/Currency/Service/Converters/EnglishNumberToWordsConverter.cs` — all existing English wording logic moved here unchanged.
  - `Backend.Api/Currency/Service/Converters/GermanNumberToWordsConverter.cs` — placeholder throwing `NotImplementedException`, with TODO comments listing German rules.
- **Changed files:**
  - `TransformCurrencyToWordsService` — reduced to an orchestrator: range validation → `DecomposedAmount.From()` → converter selection by language → delegate.
  - `ITransformCurrencyToWordsService` — signature became `ToDollars(decimal amount, Language language = Language.En)`.
  - `CurrencyController` — added `[FromQuery] Language language = Language.En`.
  - `CurrencyDomain` — registered both converters as singletons.
  - Both existing test files — updated service construction to pass the converter list.
- Fixed a build break (missing default parameter on the concrete class) and verified: **12/12 tests passing**.
- Behavior note: amounts like `0.005` now return `"zero dollars"` instead of an empty string (small bug fix via `IsZero`).

---

## 4. German test suite

**Prompt (paraphrased):** "Please write a similar test suite for the German class, as we have for the English class."

**Support given (code):**
- Created `Backend.Test/Currency/Service/TransformCurrencyToWordsServiceGermanTests.cs` mirroring the English suite one-to-one with `Language.De`, acting as a TDD spec.
- Added two German-only tests with no English equivalent: `1000000` → `eine Million Dollar` and `2000000` → `zwei Millionen Dollar` (singular/plural + *ein*→*eine*).
- Encoded expected values, e.g. `45100` → `fünfundvierzigtausendeinhundert Dollar`, `51451001` → `einundfünfzig Millionen vierhunderteinundfünfzigtausendein Dollar`.
- Verified run: 14 passing (English + German out-of-range), 9 failing with `NotImplementedException` as expected until implementation.

---

## 5. German converter implementation

**Prompt (paraphrased):** "Please implement the German numbers-to-words service."

**Support given (code):**
- Implemented `GermanNumberToWordsConverter` with German composition rules:
  - Units before tens with "und": `45` → `fünfundvierzig`.
  - Glued single words: `neunhundertneunundneunzig`, thousands glued via `tausend`.
  - `eine Million` / `zwei Millionen` as a separate capitalized word.
  - `ein` instead of `eins` (always precedes a noun or scale word).
  - Full teens table (`zehn`–`neunzehn`); no plural inflection of `Dollar`/`Cent`; `null Dollar` for zero.
- Verified: **23/23 tests passing**.

---

## Final architecture

```
CurrencyController (?amount=&language=)
    └── TransformCurrencyToWordsService        (DI, orchestrator: validation + selection)
            ├── DecomposedAmount.From(decimal) (plain record struct, language-agnostic math)
            └── INumberToWordsConverter        (DI collection, selected by Language)
                    ├── EnglishNumberToWordsConverter
                    └── GermanNumberToWordsConverter
```

