using CalculatorService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Core.Commands
{
    public class MultiplyCommand : ICommand
    {
        public async Task<long> ExecuteAsync(long left, long rigth)
        {
            await Task.Delay(50);
            return left * rigth;
        }
    }
}
