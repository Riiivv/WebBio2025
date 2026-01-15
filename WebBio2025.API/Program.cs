using WebBio2025.Application.Interfaces;
using WebBio2025.Application.Services;
using WebBio2025.Application.Services.WebBio2025.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IHall, HallRepository>();
builder.Services.AddScoped<IMovies, MoviesRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ISeat, SeatRepository>();
builder.Services.AddScoped<IShowtime, ShowtimeRepository>();
builder.Services.AddScoped<ITicket, TicketRepository>();
builder.Services.AddScoped<ISeatHold, SeatHoldRepository>();

builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IShowtimeSeatService, ShowtimeSeatService>();




builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    // CORS only in dev
    app.UseCors("DevCors");
}

// NOTE: CORS must run before routing endpoints (MapControllers) and before redirects/auth issues
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
