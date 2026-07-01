using TraineeManagementApi.Services;
using TraineeManagementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Models;
using TraineeManagementApi.Context;
using TraineeManagementApi.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using TraineeManagement.Api.ExceptionMiddlewares;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins ="_myAllowedSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://localhost:5173,https://localhost:7235/")
                                .AllowCredentials();
                      });
});

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String not found");
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.contextconfiguration(connectionString);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration=builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName=builder.Configuration.GetConnectionString("RedisInstanceName");
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {   EndPoints={builder.Configuration.GetConnectionString("RedisConnection").Split(",")[0]},
        ConnectRetry=1,
        ConnectTimeout=2000,
    };
});
builder.Services.AddControllers();
builder.Services.applicationServices();

builder.Services.AuthenticationConfig(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
        ctx.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
        ctx.ProblemDetails.Instance = $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}";
    };
});
var rabbitMqSection = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddSingleton(sp => new ConnectionFactory()
{
    HostName = rabbitMqSection["Host"]!,
    Port = Convert.ToInt32(rabbitMqSection["Port"]),
    UserName = rabbitMqSection["UserName"]!,
    Password = rabbitMqSection["Password"]!,
    
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<ApiKeySecuritySchemeTransformer>();
});

builder.Services.AddHealthChecksExtensions(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}


app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks(
    "/health/live",
    new HealthCheckOptions
    {
        Predicate = _ => false
    }
);

app.MapHealthChecks(
    "/health/ready",
    new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = HealthCheckReporter.WriteHealthCheckResponse
    }
);

await app.MigrateDatabse();

app.Run();
