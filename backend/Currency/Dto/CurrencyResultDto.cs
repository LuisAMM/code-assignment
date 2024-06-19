
namespace backend.Currency.Dto;

public class CurrencyResultDto(string currency)
{
    public string Currency { get; } = currency;
}