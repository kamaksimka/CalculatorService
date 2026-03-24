using CalculatorService.Core.Commands;
using CalculatorService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Core.Services
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(string op)
        {
            ICommand command = op switch
            {
                "+" => new AddCommand(),
                "-" => new SubtractCommand(),
                "*" => new MultiplyCommand(),
                _ => throw new NotSupportedException(op)
            };

            return command;
        }
    }
}
