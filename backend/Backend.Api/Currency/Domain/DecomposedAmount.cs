namespace Backend.Api.Currency.Domain;

/// <summary>
/// Language-agnostic decomposition of a currency amount into its scale parts.
/// </summary>
public readonly record struct DecomposedAmount(long Millions, long Thousands, long Hundreds, int Cents)
{
    private const int Million = 1000000;
    private const int Thousand = 1000;
    private const int HundredFactor = 100;

    public static DecomposedAmount From(decimal amount)
    {
        var intAmount = Convert.ToInt64(Math.Floor(amount));
        var millions = intAmount / Million; // 0 - 999
        var thousands = intAmount % Million / Thousand; // 0 - 999
        var hundreds = intAmount % Thousand; // 0 - 999
        var cents = Convert.ToInt32(Math.Floor(amount % 1 * HundredFactor)); // 0 - 99

        return new DecomposedAmount(millions, thousands, hundreds, cents);
    }

    public bool IsZero => Millions == 0 && Thousands == 0 && Hundreds == 0 && Cents == 0;

    public bool HasWholePart => Millions > 0 || Thousands > 0 || Hundreds > 0;

    public bool WholePartIsOne => Millions == 0 && Thousands == 0 && Hundreds == 1;
}

