using CalculatorService.Core.Models;

namespace CalculatorService.API.Models
{
    public class ExecuteResult
    {
        public List<PrintResult> Items { get; set; } = new List<PrintResult>();
    }
}
