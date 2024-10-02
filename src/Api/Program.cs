using Api.Extensions;
using Application.IoC;
using Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Market Zone API running!");

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

await app.ApplyMigration();

app.UseHttpsRedirection();

app.Run();
