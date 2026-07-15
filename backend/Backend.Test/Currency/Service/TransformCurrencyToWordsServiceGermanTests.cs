using Backend.Api.Currency.Domain;
using Backend.Api.Currency.Domain.Errors;
using Backend.Api.Currency.Service;
using Backend.Api.Currency.Service.Converters;

namespace Backend.Test.Currency.Service;

public class TransformCurrencyToWordsServiceGermanTests
{
    private readonly TransformCurrencyToWordsService _sut = new([new EnglishNumberToWordsConverter(), new GermanNumberToWordsConverter()]);

    [Fact]
    public void ToDollars_ReturnsError_WhenNumberIsGreaterThanMax()
    {
        // Arrange
        const decimal number = 1000000000;

        // Act
        var result = _sut.ToDollars(number, Language.De);

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
        var result = _sut.ToDollars(number, Language.De);

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
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("null Dollar");
    }

    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIsMax()
    {
        // Arrange
        const decimal number = 999999999.99m;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("neunhundertneunundneunzig Millionen neunhundertneunundneunzigtausendneunhundertneunundneunzig Dollar und neunundneunzig Cent");
    }

    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs1()
    {
        // Arrange
        const decimal number = 1;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("ein Dollar");
    }

    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs0_01()
    {
        // Arrange
        const decimal number = 0.01m;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("null Dollar und ein Cent");
    }

    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs45100()
    {
        // Arrange
        const decimal number = 45100m;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("fünfundvierzigtausendeinhundert Dollar");
    }

    [Fact]
    public void ToDollars_ReturnsProperString_WhenNumberIs25_1()
    {
        // Arrange
        const decimal number = 25.1m;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("fünfundzwanzig Dollar und zehn Cent");
    }

    [Fact]
    public void ToDollars_ReturnsProperString_WhenHundredsIsOne()
    {
        // Arrange
        const decimal number = 51451001;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("einundfünfzig Millionen vierhunderteinundfünfzigtausendein Dollar");
    }

    [Fact]
    public void ToDollars_ReturnsSingularMillion_WhenMillionsIsOne()
    {
        // Arrange
        const decimal number = 1000000;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("eine Million Dollar");
    }

    [Fact]
    public void ToDollars_ReturnsPluralMillionen_WhenMillionsIsGreaterThanOne()
    {
        // Arrange
        const decimal number = 2000000;

        // Act
        var result = _sut.ToDollars(number, Language.De);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe("zwei Millionen Dollar");
    }
}

