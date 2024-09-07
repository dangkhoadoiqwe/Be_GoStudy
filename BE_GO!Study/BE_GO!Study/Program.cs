using BE_GO_Study.AppStart;
using BE_GO_Study.GlobalExceptionHandler;
using DataAccess.Model;
using DataAccess.Repositories;
using FSAM.BusinessLogic.Generations.DependencyInjection;
using GO_Study_Logic.Helper.CustomExceptions;
using GO_Study_Logic.Service;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the DbContext with SQL Server
builder.Services.AddDbContext<GOStudyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity for user management (if needed)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GOStudyContext>();

// Add AutoMapper for dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Initialize Dependency Injection
builder.Services.InitializerDependencyInjection();

// Configure AutoMapper
builder.Services.ConfigureAutoMapper();

// JWT Key from configuration
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Key"]
    ?? throw new Exception("JwtSettings:Key is not found"));

// Token validation parameters for JWT
var tokenValidationParameter = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    RequireExpirationTime = true,
    ClockSkew = TimeSpan.Zero
};

// Authentication configuration (JWT and Google)
builder.Services.AddAuthentication(options =>
{
    // Default scheme is JWT Bearer
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = async context =>
        {
            throw new UnauthourizeException("Failed to validate token");
        }
    };
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameter;
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"];   // Thay bằng ClientId của bạn
    options.ClientSecret = builder.Configuration["Google:ClientSecret"]; // Thay bằng ClientSecret của bạn
});

builder.Services.AddSingleton(tokenValidationParameter);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<ArgumentExceptionHandlingMiddleware>();
builder.Services.AddTransient<UnauthourizeExceptionHandlingMiddleware>();
builder.Services.AddHttpClient();
// Add MVC controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "GOStudy", Version = "v1" });

    // Define the JWT Bearer scheme that's used across the API
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
