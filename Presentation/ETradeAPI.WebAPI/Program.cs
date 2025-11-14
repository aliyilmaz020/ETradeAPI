using ETradeAPI.Application;
using ETradeAPI.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddValidationServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("api-docs");
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
