using AmazingCalculator;
using FluentAssertions;
using Xunit;

namespace AmazingCalculator.Tests.Unit;

public class IntCalculatorTests
{
    [Theory]
    [ClassData(typeof(AddData))]
    public void Add_ShouldAddTwoNumbers_WhenBothOfThemAreIntegers(
        int a, int b, int expected)
    {
        // Arrange
        var sut = new IntCalculator();
        
        // Act
        var result = sut.Add(a, b);

        //Assert
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTheNumbersAreIntegers()
    {
        // Arrange
        var calculator = new IntCalculator();

        // Act
        var result = calculator.Subtract(7, 5);

        // Assert
        result.Should().Be(2);
    }
    
    [Fact]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTheNumbersArePositiveIntegers()
    {
        // Arrange
        var calculator = new IntCalculator();

        // Act
        var result = calculator.Multiply(6, 9);
    
        // Assert
        result.Should().Be(54);
    }
    
    [Fact]
    public void Multiply_ShouldReturnZero_WhenOneOfTheNumbersIsZero()
    {
        // Arrange
        var calculator = new IntCalculator();

        // Act
        var result = calculator.Multiply(7, 0);
    
        // Assert
        result.Should().Be(0);
    }
    
    [Fact]
    public void Divide_ShouldDivideTwoNumbers_WhenNumbersAreDivisible()
    {
        // Arrange
        var calculator = new IntCalculator();

        // Act
        var result = calculator.Divide(10, 2);
    
        // Assert
        result.Should().Be(5);
    }
    
    [Fact]
    public void Divide_ShouldReturnTheFirstNumber_WhenNumberIsDividedByOne()
    {
        // Arrange
        var calculator = new IntCalculator();

        // Act
        var result = calculator.Divide(7, 1);
    
        // Assert
        result.Should().Be(7);
    }

    public static IEnumerable<object[]> AddData =>
    [
        [1, 2, 3],
        [5, -5, 0]
    ];
}
