using Backend.Api.Currency.Domain;

namespace Backend.Api.Currency.Service.Converters;

public class EnglishNumberToWordsConverter : INumberToWordsConverter
{
    private const int Hundred = 100;
    private const int Ten = 10;

    public Language Language => Language.En;

    public string ToWords(DecomposedAmount amount)
    {
        if (amount.IsZero)
        {
            return "zero dollars";
        }

        var stringResult = "";

        if (amount.Millions > 0)
        {
            stringResult += $"{HundredsToWords(amount.Millions)} million ";
        }

        if (amount.Thousands > 0)
        {
            stringResult += $"{HundredsToWords(amount.Thousands)} thousand ";
        }

        if (amount.Hundreds > 0)
        {
            stringResult += $"{HundredsToWords(amount.Hundreds)} dollar{(amount.WholePartIsOne ? "" : "s")} ";
        }

        if (amount.Cents > 0)
        {
            if (!amount.HasWholePart)
            {
                stringResult = "zero dollars ";
            }
            stringResult += $"and {HundredsToWords(amount.Cents)} cent{(amount.Cents > 1 ? "s" : "")}";
        }

        return stringResult.Trim();
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
            result += $"{TensToWords(hundreds)} hundred";
        }
        if (tens > 0)
        {
            result += hundreds > 0 ? $" {TensToWords(tens)}" : TensToWords(tens);
        }

        return result;
    }

    private static string TensToWords(long number)
    {
        if (number is < 0 or > 99)
        {
            return "Invalid number";
        }

        string[] ones = ["", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        string[] teens = ["", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"];
        string[] tens = ["", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"];

        var words = "";

        var tenDivision = number / Ten;
        var tenRemainder = number % Ten;

        if (tenDivision == 1 && tenRemainder > 0)
        {
            words = teens[tenRemainder];
        }
        else if (tenDivision >= 1)
        {
            words = tenRemainder == 0 ? tens[tenDivision] : $"{tens[tenDivision]}-{ones[tenRemainder]}";
        }
        else
        {
            words = ones[tenRemainder];
        }
        return words;
    }
}

