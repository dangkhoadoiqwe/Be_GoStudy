// Program.cs or Startup.cs (ASP.NET Core setup file)
using BE_GO_Study.AppStart;
using DataAccess.Model;
using DataAccess.Repositories;
using FSAM.BusinessLogic.Generations.DependencyInjection;
using GO_Study_Logic.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the DbContext with SQL Server
builder.Services.AddDbContext<GOStudyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper for dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Initialize Dependency Injection
builder.Services.InitializerDependencyInjection();

// Configure AutoMapper
builder.Services.ConfigureAutoMapper();

// Register TokenValidationParameters
builder.Services.AddSingleton<TokenValidationParameters>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var jwtSettings = configuration.GetSection("JwtSettings");
    return new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true, 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// Add services for controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
