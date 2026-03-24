using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Core.Models
{
    public class Instruction
    {
        public string Type { get; set; } = string.Empty;
        public string? Op { get; set; }
        public string? Var { get; set; }
        public object? Left { get; set; }
        public object? Right { get; set; }
    }
}
