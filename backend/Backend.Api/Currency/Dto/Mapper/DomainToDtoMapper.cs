using Backend.Api.Currency.Domain;

namespace Backend.Api.Currency.Dto.Mapper;

public static class DomainToDtoMapper
{
    public static CurrencyResultDto ToDto(this CurrencyResult currencyResult) => new(currencyResult.Value);
}