using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Models;

namespace CalculatorService.Core.Services;

public class InstructionExecutor: IInstructionExecutor
{
    private readonly ICommandFactory _commandFactory;

    public InstructionExecutor(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }

    public async Task<List<PrintResult>> ExecuteInstructionsAsync(List<Instruction> instructions)
    {
        var calculator = new CalculatorEngine(_commandFactory,instructions);

        return await calculator.ExecuteAsync();
    }
}