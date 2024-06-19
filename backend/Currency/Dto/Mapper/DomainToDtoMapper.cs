using backend.Currency.Domain;

namespace backend.Currency.Dto.Mapper;

public static class DomainToDtoMapper
{
    public static CurrencyResultDto ToDto(this CurrencyResult currencyResult) => new(currencyResult.Value);
}