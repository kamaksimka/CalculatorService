using Xunit;
using CalculatorService.Core.Commands;
using CalculatorService.Core.Interfaces;
using System.Threading.Tasks;

namespace CalculatorService.Tests.UnitTests;

public class AddCommandTests
{
    [Fact]
    public async Task AddCommand_ExecuteAsync_ShouldAddCorrectly()
    {
        // Arrange
        var command = new AddCommand();

        // Act
        var result = await command.ExecuteAsync(5, 3);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public async Task AddCommand_ExecuteAsync_WithNegativeNumbers_ShouldAddCorrectly()
    {
        // Arrange
        var command = new AddCommand();

        // Act
        var result = await command.ExecuteAsync(-5, 3);

        // Assert
        Assert.Equal(-2, result);
    }

    [Fact]
    public async Task AddCommand_ExecuteAsync_WithLargeNumbers_ShouldAddCorrectly()
    {
        // Arrange
        var command = new AddCommand();

        // Act
        var result = await command.ExecuteAsync(long.MaxValue - 1, 1);

        // Assert
        Assert.Equal(long.MaxValue, result);
    }
}