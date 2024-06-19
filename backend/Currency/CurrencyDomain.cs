using backend.Currency.Service;

namespace backend.Currency;

public static class CurrencyDomain
{
    public static IServiceCollection AddCurrency(this IServiceCollection services)
        => services.AddTransient<ITransformCurrencyToWordsService, TransformCurrencyToWordsService>();
}