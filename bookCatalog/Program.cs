using System.Net;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var connectionString = "Host=postgres;Port=5432;Database=bookCatalog;Username=postgres;Password=bookCatalogPassword";
builder.Services.AddDbContext<BookCatalogDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IRepository<BookDTO>, BookPostgreRepository>();
builder.Services.AddScoped<IRepository<AuthorDTO>, AuthorPostgreRepository>();
builder.Services.AddScoped<IRepository<GenreDTO>, GenrePostgreRepository>();

builder.Services.AddScoped<FileService>();

builder.Services.AddControllers();

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

app.ApplyMigrations();
app.UseCors();
app.MapControllers();
app.Run();
