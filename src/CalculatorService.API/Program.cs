using CalculatorService.Core.Interfaces;
using CalculatorService.Core.Services;
using CalculatorService.API.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register core services
builder.Services.AddSingleton<ICommandFactory, CommandFactory>();
builder.Services.AddSingleton<InstructionExecutor>();

// Add gRPC
//builder.Services.AddGrpc();

var app = builder.Build();

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Map gRPC service
app.MapGrpcService<CalculatorGrpcService>();

app.Run();