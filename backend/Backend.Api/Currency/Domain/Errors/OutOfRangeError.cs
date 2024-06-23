using FluentResults;

namespace Backend.Api.Currency.Domain.Errors;

public class OutOfRangeError : Error
{
    public OutOfRangeError(decimal amount) 
        : base("Amount out of range")
    {
        WithMetadata("Amount", amount);
    }
}