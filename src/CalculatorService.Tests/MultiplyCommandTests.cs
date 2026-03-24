using Xunit;
using CalculatorService.Core.Commands;
using CalculatorService.Core.Interfaces;
using System.Threading.Tasks;

namespace CalculatorService.Tests.UnitTests;

public class MultiplyCommandTests
{
    [Fact]
    public async Task MultiplyCommand_ExecuteAsync_ShouldMultiplyCorrectly()
    {
        // Arrange
        var command = new MultiplyCommand();

        // Act
        var result = await command.ExecuteAsync(4, 5);

        // Assert
        Assert.Equal(20, result);
    }

    [Fact]
    public async Task MultiplyCommand_ExecuteAsync_WithZero_ShouldReturnZero()
    {
        // Arrange
        var command = new MultiplyCommand();

        // Act
        var result = await command.ExecuteAsync(100, 0);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task MultiplyCommand_ExecuteAsync_WithNegativeNumbers_ShouldMultiplyCorrectly()
    {
        // Arrange
        var command = new MultiplyCommand();

        // Act
        var result = await command.ExecuteAsync(-4, 5);

        // Assert
        Assert.Equal(-20, result);
    }

    [Fact]
    public async Task MultiplyCommand_ExecuteAsync_WithLargeNumbers_ShouldMultiplyCorrectly()
    {
        // Arrange
        var command = new MultiplyCommand();

        // Act
        var result = await command.ExecuteAsync(1_000_000, 1_000_000);

        // Assert
        Assert.Equal(1_000_000_000_000, result);
    }
}