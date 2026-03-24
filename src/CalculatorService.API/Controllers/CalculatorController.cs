using Microsoft.AspNetCore.Mvc;
using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Models;
using System.Text.Json;
using CalculatorService.Core.Services;

namespace CalculatorService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly InstructionExecutor _executor;

    public CalculatorController(InstructionExecutor executor)
    {
        _executor = executor;
    }

    [HttpPost("execute")]
    public async Task<IActionResult> Execute([FromBody] List<Instruction> instructions)
    {
        try
        {
            var result = await _executor.ExecuteInstructionsAsync(instructions);
            return Ok(new { items = result.Select(r => new { var = r.Var, value = r.Value }) });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}