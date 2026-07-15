using Backend.Api.Currency.Domain;

namespace Backend.Api.Currency.Service.Converters;

public class GermanNumberToWordsConverter : INumberToWordsConverter
{
    public Language Language => Language.De;

    public string ToWords(DecomposedAmount amount)
    {
        // TODO: implement German number-to-words conversion.
        // Reminders:
        // - Units come before tens: 23 -> "dreiundzwanzig"
        // - Numbers are written as one word: 123 -> "einhundertdreiundzwanzig"
        // - "one" changes form: "ein Dollar" vs "eins"
        // - Millions are a separate, pluralized word: "eine Million" / "zwei Millionen"
        // - Thousands are glued: 203000 -> "zweihundertdreitausend"
        throw new NotImplementedException("German conversion is not implemented yet.");
    }
}

