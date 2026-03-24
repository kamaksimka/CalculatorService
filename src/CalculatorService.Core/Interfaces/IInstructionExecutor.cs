using CalculatorService.Core.Models;

namespace CalculatorService.Core.Interfaces
{
    public interface IInstructionExecutor
    {
        Task<List<PrintResult>> ExecuteInstructionsAsync(List<Instruction> instructions);
    }
}
