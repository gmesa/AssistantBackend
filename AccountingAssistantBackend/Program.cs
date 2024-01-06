using AccountingAssistantBackend.Extensions;
using AccountingAssistantBackend.Infrastructure.Configuration;
using AccountingAssistantBackend.Middlewares;
using AccountingAssistantBackend.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;


var builder = WebApplication.CreateBuilder(args);

var config = new ConfigManager(builder.Configuration);

// Add services to the container.
builder.Services
    .AddSingleton<IConfigManager, ConfigManager>()
    .AddCustomHandlerExceptionConfiguration(builder.Configuration)
    .AddSemanticKernelWithChatCompletions(config)
.AddControllers();

builder.Services
    .AddScoped<IAccountingManager, AccountingManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors configuration
builder.Services
    .AddCors(option =>
    {
        option.AddPolicy("cors", builder =>
        {
            builder
            .WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseCors("cors");
app.UseAuthorization();

app.MapControllers();

app.Run();
