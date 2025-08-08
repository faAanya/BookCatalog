using System.Net;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var connectionString = Environment.GetEnvironmentVariable("POSTGRES_STRING");
builder.Services.AddDbContext<BookCatalogDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IRepository<Book>, BookPostgreRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorPostgreRepository>();
builder.Services.AddScoped<IRepository<Genre>, GenrePostgreRepository>();

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

app.UseCors();
app.MapControllers();
app.Run();
