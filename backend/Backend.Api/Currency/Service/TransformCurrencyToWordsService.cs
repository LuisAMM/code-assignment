using Backend.Api.Currency.Domain;
using Backend.Api.Currency.Domain.Errors;
using Backend.Api.Currency.Service.Converters;
using FluentResults;

namespace Backend.Api.Currency.Service;

public class TransformCurrencyToWordsService(IEnumerable<INumberToWordsConverter> converters) : ITransformCurrencyToWordsService
{
    public Result<CurrencyResult> ToDollars(decimal amount, Language language = Language.En)
    {
        if (amount is < 0 or >= 1000000000)
        {
            return Result.Fail(new OutOfRangeError(amount));
        }

        var converter = converters.Single(c => c.Language == language);
        var decomposedAmount = DecomposedAmount.From(amount);

        return new CurrencyResult(converter.ToWords(decomposedAmount));
    }
}