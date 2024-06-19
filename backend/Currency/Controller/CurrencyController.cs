using backend.Currency.Domain.Errors;
using backend.Currency.Dto;
using backend.Currency.Dto.Mapper;
using backend.Currency.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Currency.Controller;

[ApiController]
[Route("api/currency")]
public class CurrencyController(ITransformCurrencyToWordsService transformCurrencyToWordsService) : ControllerBase
{
    [HttpGet]
    public Results<Ok<CurrencyResultDto>, BadRequest<CurrencyError>> ConvertToDollar([FromQuery] long amount)
    {
        var result = transformCurrencyToWordsService.ToDollars(amount);
        if (result.IsFailed)
        {
            return result switch
            {
                _ when result.HasError(e => e is OutOfRangeError) => TypedResults.BadRequest(
                    new CurrencyError(ErrorType.OutOfRange)),
                _ => TypedResults.BadRequest(new CurrencyError(ErrorType.Generic))
            };
        }

        return TypedResults.Ok(result.Value.ToDto());
    }
}