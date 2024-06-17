using API;
using Application;

using FluentMigrator.Runner;


using Infrastructure;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrasturcture(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

try
{
    using var serviceScope = app.Services.CreateScope();
    var services = serviceScope.ServiceProvider;
    var runner = services.GetRequiredService<IMigrationRunner>();

    runner.MigrateUp();
}
catch (Exception e)
{
    app.Logger.LogError(e, "Error when migrating the database");
}

app.Run();