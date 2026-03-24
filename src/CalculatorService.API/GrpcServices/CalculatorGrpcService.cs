using Calculator;
using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Models;
using CalculatorService.Core.Services;
using Grpc.Core;
using System.Net.NetworkInformation;

namespace CalculatorService.API.GrpcServices;

public class CalculatorGrpcService : CalculatorServiceGrpc.CalculatorServiceGrpcBase
{
    private readonly IInstructionExecutor _executor;
    private readonly ILogger<CalculatorGrpcService> _logger;

    public CalculatorGrpcService(IInstructionExecutor executor, ILogger<CalculatorGrpcService> logger)
    {
        _executor = executor;
        _logger = logger;
    }

    public override async Task<PrintResultListGrpc> ExecuteInstructions(InstructionListGrpc request, ServerCallContext context)
    {
        try
        {
            var instructions = ConvertToCoreInstructions(request.Instructions);
            var result = await _executor.ExecuteInstructionsAsync(instructions);

            var response = new PrintResultListGrpc();
            foreach (var r in result)
            {
                response.Items.Add(new PrintResultGrpc { Var = r.Var, Value = r.Value });
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing instructions");
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    private List<Instruction> ConvertToCoreInstructions(IEnumerable<InstructionGrpc> grpcInstructions)
    {
        var instructions = new List<Instruction>();

        foreach (var grpcInst in grpcInstructions)
        {
            var instruction = new Instruction
            {
                Type = grpcInst.Type,
                Op = grpcInst.Op,
                Var = grpcInst.Var
            };

            if (grpcInst.LeftCase == InstructionGrpc.LeftOneofCase.LeftInt)
                instruction.Left = grpcInst.LeftInt;
            else if (grpcInst.LeftCase == InstructionGrpc.LeftOneofCase.LeftVar)
                instruction.Left = grpcInst.LeftVar;

            if (grpcInst.RightCase == InstructionGrpc.RightOneofCase.RightInt)
                instruction.Right = grpcInst.RightInt;
            else if (grpcInst.RightCase == InstructionGrpc.RightOneofCase.RightVar)
                instruction.Right = grpcInst.RightVar;

            instructions.Add(instruction);
        }

        return instructions;
    }
}