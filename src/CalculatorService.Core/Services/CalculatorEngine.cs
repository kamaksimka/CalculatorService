using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Models;
using System.Collections.Concurrent;

public class CalculatorEngine
{
    private readonly ICommandFactory _commandFactory;
    private readonly Dictionary<string, Instruction> _calcInstructions;
    private readonly List<string> _printVariables;

    // Кэшируем именно Task<long>, чтобы ожидающие потоки подключались к уже запущенной задаче
    private readonly ConcurrentDictionary<string, Task<long>> _taskCache = new();

    public CalculatorEngine(ICommandFactory commandFactory, List<Instruction> instructions)
    {
        _commandFactory = commandFactory;

        _calcInstructions = instructions
            .Where(i => i.Type == "calc" && !string.IsNullOrEmpty(i.Var))
            .ToDictionary(i => i.Var!);

        _printVariables = instructions
            .Where(i => i.Type == "print" && !string.IsNullOrEmpty(i.Var))
            .Select(i => i.Var!)
            .ToList();
    }

    public async Task<List<PrintResult>> ExecuteAsync()
    {
        // Запускаем все целевые переменные параллельно
        var tasks = _printVariables.Select(async p =>
            new PrintResult(p, await ResolveVar(p)));

        return (await Task.WhenAll(tasks)).ToList();
    }

    private Task<long> ResolveVar(string varName)
    {
        // GetOrAdd гарантирует, что для одного varName будет создана только одна задача
        return _taskCache.GetOrAdd(varName, async name =>
        {
            if (!_calcInstructions.TryGetValue(name, out var instr))
            {
                throw new KeyNotFoundException($"Переменная '{name}' не определена в инструкциях calc.");
            }

            // Запускаем получение левого и правого операнда параллельно
            var leftTask = GetValue(instr.Left);
            var rightTask = GetValue(instr.Right);

            await Task.WhenAll(leftTask, rightTask);


            var command = _commandFactory.CreateCommand(instr.Op);

            return await command.ExecuteAsync(leftTask.Result, rightTask.Result);
        });
    }

    private async Task<long> GetValue(object? operand)
    {
        if (operand == null) throw new ArgumentNullException(nameof(operand));

        var operandStr = operand.ToString();

        // Если это число — возвращаем сразу
        if (long.TryParse(operandStr, out long value))
        {
            return value;
        }

        // Если это имя переменной — рекурсивно разрешаем её
        return await ResolveVar(operandStr!);
    }
}
