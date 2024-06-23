using Backend.Api.Currency.Controller;
using Backend.Api.Currency.Dto;
using Backend.Api.Currency.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Test.Currency.Controller;

public class GetCurrencyControllerTests
{
    private readonly CurrencyController _sut;
    
    public GetCurrencyControllerTests()
    {
        var transformCurrencyToWordsService = new TransformCurrencyToWordsService();
        _sut = new CurrencyController(transformCurrencyToWordsService);
    }   
    
    [Fact]
    public void ConvertToDollar_ReturnsBadRequest_WhenAmountIsGreaterThanMax()
    {
        // Arrange
        var amount = 1000000000;
        
        // Act
        var result = _sut.ConvertToDollar(amount);
        
        // Assert
        var castedResult = result.Result as BadRequest<CurrencyError>;
        castedResult.ShouldNotBeNull();
        castedResult.Value.ShouldNotBeNull();
        castedResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        castedResult.Value.ErrorType.ShouldBe(ErrorType.OutOfRange);
    }
    
    [Fact]
    public void ConvertToDollar_ReturnsBadRequest_WhenAmountIsLowerThanMin()
    {
        // Arrange
        var amount = -1;
        
        // Act
        var result = _sut.ConvertToDollar(amount);
        
        // Assert
        var castedResult = result.Result as BadRequest<CurrencyError>;
        castedResult.ShouldNotBeNull();
        castedResult.Value.ShouldNotBeNull();
        castedResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        castedResult.Value.ErrorType.ShouldBe(ErrorType.OutOfRange);
    }
    
    [Fact]
    public void ConvertToDollar_ReturnsOk_WithProperInput()
    {
        // Arrange
        const decimal amount = 23548.1m;
        
        // Act
        var result = _sut.ConvertToDollar(amount);
        
        // Assert
        var castedResult = result.Result as Ok<CurrencyResultDto>;
        castedResult.ShouldNotBeNull();
        castedResult.Value.ShouldNotBeNull();
        castedResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
        castedResult.Value.Currency.ShouldBe("twenty-three thousand five hundred forty-eight dollars and ten cents");
    }
}