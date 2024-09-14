
using BE_GOStudy.DependencyInjection;
using BE_GOStudy.GlobalExceptionHandler;
using DataAccess.Model;
using DataAccess.Repositories;
using BE_GOStudy.DependencyInjection;
using GO_Study_Logic.Helper.CustomExceptions;
using GO_Study_Logic.Service;
using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BE_GOStudy.AppStart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the DbContext with SQL Server
builder.Services.AddDbContext<GOStudyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity for user management
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<GOStudyContext>();

// Add AutoMapper for dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register MediatR - ensuring the correct assembly is targeted
// Đăng ký MediatR và các handler
builder.Services.AddMediatR(typeof(CreatePaymentHandler).Assembly);

builder.Services.AddHttpContextAccessor();
// Initialize Dependency Injection
builder.Services.InitializerDependencyInjection();

// Configure AutoMapper (assuming this is a custom method)
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

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Nếu cần sử dụng cookie hoặc thông tin xác thực khác
    });
});

// Authentication configuration (JWT and Google)
builder.Services.AddAuthentication(options =>
{
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
    options.ClientId = builder.Configuration["Google:ClientId"];
    options.ClientSecret = builder.Configuration["Google:ClientSecret"];
});

builder.Services.AddSingleton(tokenValidationParameter);

// Register middleware components
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<ArgumentExceptionHandlingMiddleware>();
builder.Services.AddTransient<UnauthourizeExceptionHandlingMiddleware>();
builder.Services.AddHttpClient();

// Add MVC controllers
builder.Services.AddControllers();
builder.Services.Configure<VnpayConfig>(
             builder.Configuration.GetSection(VnpayConfig.ConfigName));
// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "GOStudy", Version = "v1" });

    // Define the JWT Bearer scheme used across the API
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

app.UseCors("AllowReactApp"); // Sử dụng policy CORS
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
