using backend.Currency.Domain;
using FluentResults;

namespace backend.Currency.Service;

public class TransformCurrencyToWordsService : ITransformCurrencyToWordsService
{
    public Result<CurrencyResult> ToDollars(long amount)
    {
        return new CurrencyResult("Zero");
    }
}