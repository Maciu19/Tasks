using API;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrasturcture(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();