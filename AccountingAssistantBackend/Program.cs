using AccountingAssistantBackend.Data.Context;
using AccountingAssistantBackend.Data.Repository;
using AccountingAssistantBackend.Extensions;
using AccountingAssistantBackend.Infrastructure.Configuration;
using AccountingAssistantBackend.Middlewares;
using AccountingAssistantBackend.Services;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var config = new ConfigManager(builder.Configuration);

// Add services to the container.
builder.Services
    .AddSingleton<IConfigManager, ConfigManager>()
    .AddCustomHandlerExceptionConfiguration(builder.Configuration)
    .AddSemanticKernelWithChatCompletions(config)
    .AddAutomapperProfile()
    .AddControllers();

builder.Services
    .AddSwaggerGen().ConfigureOptions<ConfigureSwaggerGenOptions>()
    .AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
        opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                        new HeaderApiVersionReader("x-api-version"),
                                                        new MediaTypeApiVersionReader("x-api-version"));
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

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

builder.Services
    .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Assistant"));

builder.Services
    .AddScoped<IAssistantManager, AssistantManager>()
    .AddScoped<ISessionChatManager, SessionChatManager>()
    .AddScoped<IChatMessageManager, ChatMessageManager>()
    .AddScoped<ISessionChatRepository, SessionChatRepository>()
    .AddScoped<IChatMessageRepository, ChatMessageRepository>();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {

        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var serviceScope = serviceScopeFactory.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseCors("cors");
app.UseAuthorization();

app.MapControllers();

app.Run();
