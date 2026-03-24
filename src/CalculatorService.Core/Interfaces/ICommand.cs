using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Core.Interfaces
{
    public interface ICommand
    {
        Task<long> ExecuteAsync(long left, long rigth);
    }
}
