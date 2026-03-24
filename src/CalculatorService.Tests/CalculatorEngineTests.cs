using Xunit;
using CalculatorService.Core.Services;
using CalculatorService.Core.Models;

namespace CalculatorService.Tests.UnitTests;

public class CalculatorEngineTests
{
    [Fact]
    public async Task ExecuteAsync_SimpleAddition_ReturnsCorrectValue()
    {
        // Arrange
        var commandFactory = new CommandFactory();
       
        var instructions = new List<Instruction>
        {
            new() { Type = "calc", Op = "+", Var = "x", Left = 1L, Right = 2L },
            new() { Type = "print", Var = "x" }
        };

        var engine = new CalculatorEngine(commandFactory, instructions);

        // Act
        var results = await engine.ExecuteAsync();

        // Assert
        Assert.Single(results);
        Assert.Equal("x", results[0].Var);
        Assert.Equal(3, results[0].Value);
    }

    [Fact]
    public async Task ExecuteAsync_DependentVariables_CalculatesCorrectly()
    {
        // Arrange
        var commandFactory = new CommandFactory();
        
        var instructions = new List<Instruction>
        {
            new() { Type = "calc", Op = "+", Var = "x", Left = 10L, Right = 2L },
            new() { Type = "print", Var = "x" },
            new() { Type = "calc", Op = "-", Var = "y", Left = "x", Right = 3L },
            new() { Type = "calc", Op = "*", Var = "z", Left = "x", Right = "y" },
            new() { Type = "print", Var = "w" },
            new() { Type = "calc", Op = "*", Var = "w", Left = "z", Right = 0L }
        };

        var engine = new CalculatorEngine(commandFactory,instructions);

        // Act
        var results = await engine.ExecuteAsync();

        // Assert
        Assert.Equal(2, results.Count);
        Assert.Equal(12, results[0].Value);
        Assert.Equal(0, results[1].Value);
    }

    [Fact]
    public async Task ExecuteAsync_VariableAlreadySet_ThrowsException()
    {
        // Arrange
        var commandFactory = new CommandFactory();

       
        var instructions = new List<Instruction>
        {
            new() { Type = "calc", Op = "+", Var = "x", Left = 1L, Right = 2L },
            new() { Type = "calc", Op = "+", Var = "x", Left = 3L, Right = 4L }
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new CalculatorEngine(commandFactory, instructions));
    }
}