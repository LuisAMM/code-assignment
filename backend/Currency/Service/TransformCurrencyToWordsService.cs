using backend.Currency.Domain;
using backend.Currency.Domain.Errors;
using FluentResults;

namespace backend.Currency.Service;

public class TransformCurrencyToWordsService : ITransformCurrencyToWordsService
{
    private const int Million = 1000000;
    private const int Thousand = 1000;
    private const int Hundred = 100;
    private const int Ten = 10;
    private const int One = 1;
    
    public Result<CurrencyResult> ToDollars(decimal amount)
    {
        if (amount == 0)
        {
            return new CurrencyResult("zero dollars");
        }
        if(amount is < 0 or >= 1000000000)
        {
            return Result.Fail(new OutOfRangeError(amount));
        }

        var intAmount = Convert.ToInt64(Math.Floor(amount));
        var millions = intAmount / Million; //0 - 999
        var thousands = intAmount % Million / Thousand; //0 - 999 
        var hundreds = intAmount % Thousand; //0 - 999
        var tensDecimals = Convert.ToInt16(Math.Floor((amount % One) * Hundred)); //0 - 99

        var stringResult = "";
        
        if (millions > 0)
        {
            var millionsString = HundredsToWords(millions);
            stringResult += $"{millionsString} million ";
        }
        
        if(thousands > 0)
        {
            var thousandsString = HundredsToWords(thousands);
            stringResult += $"{thousandsString} thousand ";
        }
        
        if(hundreds > 0)
        {
            var hundredsString = HundredsToWords(hundreds);
            stringResult += $"{hundredsString} dollar{(hundreds > 1 ? "s" : "")} ";
        }
        
        if(tensDecimals > 0)
        {
            var tensDecimalsString = HundredsToWords(tensDecimals);
            if (amount < 1)
            {
                stringResult = "zero dollars ";
            }
            stringResult += $"and {tensDecimalsString} cent{(tensDecimals > 1 ? "s" : "")}";
        }

        return new CurrencyResult(stringResult);
    }

    private static string HundredsToWords(long number)
    {
        if(number is < 0 or > 999)
        {
            return "Invalid number";
        }
        
        var tens = number % Hundred;
        var hundreds = number / Hundred;

        var result = "";
        if(hundreds > 0)
        {
            result += $"{TensToWords(hundreds)} hundred";
        }
        if(tens > 0)
        {
            result += hundreds > 0 ? $" {TensToWords(tens)}" : TensToWords(tens);
        }

        return result;
    }
    
    private static string TensToWords(long number)
    {
        if(number is < 0 or > 99)
        {
            return "Invalid number";
        }
        
        string[] ones = ["", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        string[] teens = ["", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"];
        string[] tens = ["", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"];
        
        var words = "";
        
        var tenDivision = number / Ten;
        var tenRemainder = number % Ten;
        
        if(tenDivision == 1 && tenRemainder > 0)
        {
            words = teens[tenRemainder];
        }
        else if(tenDivision > 1)
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