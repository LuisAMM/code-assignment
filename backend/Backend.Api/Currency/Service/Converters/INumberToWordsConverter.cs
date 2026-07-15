using Backend.Api.Currency.Domain;

namespace Backend.Api.Currency.Service.Converters;

public interface INumberToWordsConverter
{
    Language Language { get; }

    string ToWords(DecomposedAmount amount);
}

