using CalculatorService.Core.Commands;
using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Services;
using System;
using Xunit;

namespace CalculatorService.Tests.UnitTests;

public class CommandFactoryTests
{
    private readonly ICommandFactory _factory;

    public CommandFactoryTests()
    {
        _factory = new CommandFactory();
    }

    [Fact]
    public void CreateCommand_ForAddOperation_ShouldReturnAddCommand()
    {
        // Act
        var command = _factory.CreateCommand("+");

        // Assert
        Assert.IsType<AddCommand>(command);
    }

    [Fact]
    public void CreateCommand_ForSubtractOperation_ShouldReturnSubtractCommand()
    {
        // Act
        var command = _factory.CreateCommand("-");

        // Assert
        Assert.IsType<SubtractCommand>(command);
    }

    [Fact]
    public void CreateCommand_ForMultiplyOperation_ShouldReturnMultiplyCommand()
    {
        // Act
        var command = _factory.CreateCommand("*");

        // Assert
        Assert.IsType<MultiplyCommand>(command);
    }

    [Fact]
    public void CreateCommand_ForDivisionOperation_ShouldThrowNotSupportedException()
    {
        // Act & Assert
        var exception = Assert.Throws<NotSupportedException>(() => _factory.CreateCommand("/"));
        Assert.Equal("/", exception.Message);
    }

    [Fact]
    public void CreateCommand_ForUnknownOperation_ShouldThrowNotSupportedException()
    {
        // Act & Assert
        var exception = Assert.Throws<NotSupportedException>(() => _factory.CreateCommand("%"));
        Assert.Equal("%", exception.Message);
    }

    [Fact]
    public void CreateCommand_ForEmptyOperation_ShouldThrowNotSupportedException()
    {
        // Act & Assert
        Assert.Throws<NotSupportedException>(() => _factory.CreateCommand(""));
    }

    [Fact]
    public void CreateCommand_ForNullOperation_ShouldThrowNotSupportedException()
    {
        // Act & Assert
        Assert.Throws<NotSupportedException>(() => _factory.CreateCommand(null));
    }
}