using Backend.Api.Currency.Domain;

namespace Backend.Api.Currency.Service.Converters;

public class GermanNumberToWordsConverter : INumberToWordsConverter
{
    private const int Hundred = 100;
    private const int Ten = 10;

    public Language Language => Language.De;

    public string ToWords(DecomposedAmount amount)
    {
        if (amount.IsZero)
        {
            return "null Dollar";
        }

        var stringResult = "";

        if (amount.HasWholePart)
        {
            // Millions are a separate, pluralized word: "eine Million" / "zwei Millionen"
            if (amount.Millions == 1)
            {
                stringResult += "eine Million ";
            }
            else if (amount.Millions > 1)
            {
                stringResult += $"{HundredsToWords(amount.Millions)} Millionen ";
            }

            // Thousands and hundreds are glued into a single word: 203000 -> "zweihundertdreitausend"
            var gluedWord = "";
            if (amount.Thousands > 0)
            {
                gluedWord += $"{HundredsToWords(amount.Thousands)}tausend";
            }
            if (amount.Hundreds > 0)
            {
                gluedWord += HundredsToWords(amount.Hundreds);
            }
            if (gluedWord.Length > 0)
            {
                stringResult += $"{gluedWord} ";
            }

            stringResult += "Dollar";
        }
        else
        {
            stringResult = "null Dollar";
        }

        if (amount.Cents > 0)
        {
            stringResult += $" und {HundredsToWords(amount.Cents)} Cent";
        }

        return stringResult;
    }

    private static string HundredsToWords(long number)
    {
        if (number is < 0 or > 999)
        {
            return "Invalid number";
        }

        var tens = number % Hundred;
        var hundreds = number / Hundred;

        var result = "";
        if (hundreds > 0)
        {
            result += $"{TensToWords(hundreds)}hundert";
        }
        if (tens > 0)
        {
            result += TensToWords(tens);
        }

        return result;
    }

    private static string TensToWords(long number)
    {
        if (number is < 0 or > 99)
        {
            return "Invalid number";
        }

        // "ein" (not "eins") because the word is always followed by a noun ("Dollar", "Cent")
        // or a scale word ("hundert", "tausend"), or the "und" of e.g. "einundzwanzig".
        string[] ones = ["", "ein", "zwei", "drei", "vier", "fünf", "sechs", "sieben", "acht", "neun"];
        string[] teens = ["zehn", "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn"];
        string[] tens = ["", "", "zwanzig", "dreißig", "vierzig", "fünfzig", "sechzig", "siebzig", "achtzig", "neunzig"];

        var tenDivision = number / Ten;
        var tenRemainder = number % Ten;

        if (tenDivision == 1)
        {
            return teens[tenRemainder];
        }
        if (tenDivision >= 2)
        {
            // Units come before tens: 45 -> "fünfundvierzig"
            return tenRemainder == 0 ? tens[tenDivision] : $"{ones[tenRemainder]}und{tens[tenDivision]}";
        }
        return ones[tenRemainder];
    }
}
