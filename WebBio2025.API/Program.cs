using WebBio2025.Application.Interfaces;
using WebBio2025.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IHall, HallRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

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


//string sql = "Server=(localdb)\\MSSQLLocalDB;Database=DecemberDb; Trusted_Connection=true; Trust Server Certificate=true; Integrated Security=true; Encrypt=True;";
//builder.Services.AddDbContext<WebBio2025.Infrastucture.DatabaseContext>(options =>
//    options.UseSqlServer(sql));