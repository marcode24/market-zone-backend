using Api.Documentation;
using Api.Extensions;
using Application.IoC;
using Asp.Versioning.ApiExplorer;
using Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", policy =>
  {
    policy
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin();
  });
});

// IoC configuration
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Market Zone API running!");

if (app.Environment.IsDevelopment())
{
  var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
  app.UseSwaggerDocumentation(provider);
}

app.UseCors("CorsPolicy");

// Apply migrations
await app.ApplyMigration();

// Custom exception handling
app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();

public partial class Program;
