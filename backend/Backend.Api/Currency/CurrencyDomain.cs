using Backend.Api.Currency.Service;
using Backend.Api.Currency.Service.Converters;

namespace Backend.Api.Currency;

public static class CurrencyDomain
{
    public static IServiceCollection AddCurrency(this IServiceCollection services)
        => services
            .AddSingleton<INumberToWordsConverter, EnglishNumberToWordsConverter>()
            .AddSingleton<INumberToWordsConverter, GermanNumberToWordsConverter>()
            .AddTransient<ITransformCurrencyToWordsService, TransformCurrencyToWordsService>();
}