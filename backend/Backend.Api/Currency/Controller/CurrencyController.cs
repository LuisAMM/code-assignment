using Backend.Api.Currency.Domain.Errors;
using Backend.Api.Currency.Dto;
using Backend.Api.Currency.Service;
using Backend.Api.Currency.Dto.Mapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Currency.Controller;

[ApiController]
[Route("api/currency")]
public class CurrencyController(ITransformCurrencyToWordsService transformCurrencyToWordsService) : ControllerBase
{
    [HttpGet]
    public Results<Ok<CurrencyResultDto>, BadRequest<CurrencyError>> ConvertToDollar([FromQuery] decimal amount)
    {
        var result = transformCurrencyToWordsService.ToDollars(amount);
        if (result.IsFailed)
        {
            return result switch
            {
                _ when result.HasError(e => e is OutOfRangeError) => TypedResults.BadRequest(new CurrencyError(ErrorType.OutOfRange)),
                _ => TypedResults.BadRequest(new CurrencyError(ErrorType.Generic))
            };
        }

        return TypedResults.Ok(result.Value.ToDto());
    }
}