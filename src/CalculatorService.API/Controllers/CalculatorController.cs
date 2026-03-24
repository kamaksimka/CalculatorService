using CalculatorService.API.Models;
using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Models;
using CalculatorService.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CalculatorService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly IInstructionExecutor _executor;

    public CalculatorController(IInstructionExecutor executor)
    {
        _executor = executor;
    }

    [HttpPost("execute")]
    public async Task<ExecuteResult> Execute([FromBody] List<Instruction> instructions)
    {
        var result = await _executor.ExecuteInstructionsAsync(instructions);
        return new ExecuteResult { Items = result };

    }
}