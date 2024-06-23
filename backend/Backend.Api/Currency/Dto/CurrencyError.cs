namespace Backend.Api.Currency.Dto;

public class CurrencyError(ErrorType errorType)
{
    public ErrorType ErrorType { get; } = errorType;
}