using Xunit;
using CalculatorService.Core.Commands;
using CalculatorService.Core.Interfaces;
using System.Threading.Tasks;

namespace CalculatorService.Tests.UnitTests;

public class SubtractCommandTests
{
    [Fact]
    public async Task SubtractCommand_ExecuteAsync_ShouldSubtractCorrectly()
    {
        // Arrange
        var command = new SubtractCommand();

        // Act
        var result = await command.ExecuteAsync(10, 3);

        // Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public async Task SubtractCommand_ExecuteAsync_WithNegativeResult_ShouldSubtractCorrectly()
    {
        // Arrange
        var command = new SubtractCommand();

        // Act
        var result = await command.ExecuteAsync(5, 10);

        // Assert
        Assert.Equal(-5, result);
    }

    [Fact]
    public async Task SubtractCommand_ExecuteAsync_WithNegativeNumbers_ShouldSubtractCorrectly()
    {
        // Arrange
        var command = new SubtractCommand();

        // Act
        var result = await command.ExecuteAsync(-5, -3);

        // Assert
        Assert.Equal(-2, result);
    }
}