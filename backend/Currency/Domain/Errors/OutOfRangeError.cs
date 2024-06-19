using FluentResults;

namespace backend.Currency.Domain.Errors;

public class OutOfRangeError : Error
{
    public OutOfRangeError(long amount) 
        : base("Amount out of range")
    {
        WithMetadata("Amount", amount);
    }
}