using System.Net;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var connectionString = "Host=postgres;Port=5432;Database=bookCatalog;Username=postgres;Password=bookCatalogPassword";
builder.Services.AddDbContext<BookCatalogDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();
app.MapBooksEndpoints();
app.MapAuthorsEndpoints();
app.MapGenresEndpoints();
app.Run();
