using dotenv.net;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var connectionString = Environment.GetEnvironmentVariable("POSTGRES_STRING");
builder.Services.AddDbContext<BookCatalogDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapBooksEndpoints();
app.Run();
