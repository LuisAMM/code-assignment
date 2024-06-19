using backend.Currency.Domain;
using FluentResults;

namespace backend.Currency.Service;

public interface ITransformCurrencyToWordsService
{
    Result<CurrencyResult> ToDollars(long amount);
}