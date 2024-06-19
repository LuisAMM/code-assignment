namespace backend.Currency.Domain;

public class CurrencyResult(string value)
{
    public string Value { get; } = value;
}