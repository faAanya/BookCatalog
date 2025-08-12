using System.Net;
using System.Text;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var connectionString = "Host=postgres;Port=5432;Database=bookCatalog;Username=postgres;Password=bookCatalogPassword";
builder.Services.AddDbContext<BookCatalogDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IRepository<BookDTO>, BookPostgreRepository>();
builder.Services.AddScoped<IRepository<AuthorDTO>, AuthorPostgreRepository>();
builder.Services.AddScoped<IRepository<GenreDTO>, GenrePostgreRepository>();
builder.Services.AddScoped<IUserRepository, UserPostgreRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<FileService>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var downloadsPath = Path.Combine(env.ContentRootPath, "Downloads");
    return new FileService(downloadsPath);
});
builder.Services.AddSingleton<TokenService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
