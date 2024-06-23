using Backend.Api.Currency.Domain;
using FluentResults;

namespace Backend.Api.Currency.Service;

public interface ITransformCurrencyToWordsService
{
    Result<CurrencyResult> ToDollars(decimal amount);
}