using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebBio2025.Domain.interfaces;
using WebBio2025.Infrastucture.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string sql = "Server=(localdb)\\MSSQLLocalDB;Database=DecemberDb; Trusted_Connection=true; " +
                "Trust Server Certificate=true; Integrated Security=true; Encrypt=True;";
builder.Services.AddDbContext<WebBio2025.Infrastucture.DatabaseContext>(options =>
    options.UseSqlServer(sql));
builder.Services.AddScoped<IPersonRepositories, PersonRepository>();
builder.Services.AddScoped<IHall, HallRepository>();
//builder.Services.AddScoped<>

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
