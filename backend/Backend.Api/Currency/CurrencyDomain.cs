using Backend.Api.Currency.Service;

namespace Backend.Api.Currency;

public static class CurrencyDomain
{
    public static IServiceCollection AddCurrency(this IServiceCollection services)
        => services.AddTransient<ITransformCurrencyToWordsService, TransformCurrencyToWordsService>();
}