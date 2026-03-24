using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Core.Models
{
    public class PrintResult
    {
        public PrintResult()
        {
            
        }

        public PrintResult(string var,long value)
        {
            Var = var;
            Value = value;
        }

        public string Var { get; set; } = string.Empty;
        public long Value { get; set; }
    }
}
