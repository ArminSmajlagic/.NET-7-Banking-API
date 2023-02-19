using FluentMigrator.Runner;
using src.Extensions;
using src.Filters;
using src.Middleware.APIVersioning;
using src.Middleware.Logging;
using src.Middleware.RequestRateLimit;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering my custom infrastructure

builder.Services.AddInfrastructure(configuration);

builder.Services.AddControllers(config => { 
    config.Filters.Add<NotFoundFilter>();
    config.Filters.Add<ExceptionFilter>();
});

var app = builder.Build();

//Adding migrations
using var scope = app.Services.CreateScope();

var migrationRunner = scope.ServiceProvider.GetService<IMigrationRunner>();

if (migrationRunner != null)
    scope.RunMigrations(migrationRunner);

//Adding my custom middleware
app.UseAPIVersion() // Adding API version to the respons header
    .UseRequestLimiting(30, 60)// Allowed 30 http request from single IP Address in 1 minute
    .UseLogging(); 

//Adding OpenAPI 3.1 Swagger UI
app.UseSwagger()
    .UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();