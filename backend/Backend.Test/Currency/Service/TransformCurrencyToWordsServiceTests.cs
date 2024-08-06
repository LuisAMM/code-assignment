using Backend.Api.Currency.Domain.Errors;
using Backend.Api.Currency.Service;

namespace Backend.Test.Currency.Service;

public class TransformCurrencyToWordsServiceTests
{
    private readonly TransformCurrencyToWordsService _sut = new();

    [Fact]
    public void ToDollars_ReturnsError_WhenNumberIsGreaterThanMax()
    {
        // Arrange
        const decimal number = 1000000000;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsFailed.ShouldBeTrue();
        result.HasError(e => e is OutOfRangeError).ShouldBeTrue();
    }

    [Fact]
    public void ToDollars_ReturnsError_WhenNumberIsLowerThanMin()
    {
        // Arrange
        const decimal number = -1;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsFailed.ShouldBeTrue();
        result.HasError(e => e is OutOfRangeError).ShouldBeTrue();
    }

    [Fact]
    public void ToDollars_ReturnsZeroDollars_WhenNumberIsZero()
    {
        // Arrange
        const decimal number = 0;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("zero dollars");
    }
    
    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIsMax()
    {
        // Arrange
        const decimal number = 999999999.99m;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents");
    }
    
    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs1()
    {
        // Arrange
        const decimal number = 1;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("one dollar");
    }
    
    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs0_01()
    {
        // Arrange
        const decimal number = 0.01m;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("zero dollars and one cent");
    }
    
    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs45100()
    {
        // Arrange
        const decimal number = 45100m;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("forty-five thousand one hundred dollars");
    }
    
    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs25_1()
    {
        // Arrange
        const decimal number = 25.1m;

        // Act
        var result = _sut.ToDollars(number);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("twenty-five dollars and ten cents");
    }

    [Fact]
    public void ToDollars_ReturnPlural_WhenHundredsIsOne()
    {
        const decimal number = 51451001;
        
        var result = _sut.ToDollars(number);
        
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("fifty-one million four hundred fifty-one thousand one dollars");
    }
}