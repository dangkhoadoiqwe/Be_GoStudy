using BE_GOStudy.AppStart;
using BE_GOStudy.DependencyInjection;
using BE_GOStudy.GlobalExceptionHandler;
using BE_GOStudy.Hubs;
using DataAccess.Model;
using GO_Study_Logic.Helper.CustomExceptions;
using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.ViewModel;
using GOStudy_Logic.Service.Sentmail;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net.payOS;
using Newtonsoft.Json;
using Quartz;
using Quartz.Spi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQL Server
builder.Services.AddDbContext<GOStudyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity for user management
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GOStudyContext>();

// Add AutoMapper for dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register PayOS
PayOS payOS = new PayOS(
    builder.Configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find Client ID"),
    builder.Configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find API Key"),
    builder.Configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find Checksum Key")
);

// Register MediatR and other services
builder.Services.AddMediatR(typeof(CreatePaymentHandler).Assembly);
builder.Services.AddHttpContextAccessor();
builder.Services.InitializerDependencyInjection();
builder.Services.ConfigureAutoMapper();
builder.Services.AddSingleton(payOS);
builder.Services.AddSignalR(); // Thêm SignalR

// Register TaskReminderJob for Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Cấu hình Quartz để thực hiện job TaskReminderJob mỗi giờ
    var jobKey = new JobKey("taskReminderJob");

    q.AddJob<TaskReminderJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)  // Liên kết trigger với TaskReminderJob
        .WithIdentity("taskReminderTrigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(1)  // Chạy mỗi giờ
            .RepeatForever()));
});

// Thêm Quartz làm dịch vụ background
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// JWT Key from configuration
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Key"]
    ?? throw new Exception("JwtSettings:Key is not found"));

// Token validation parameters for JWT
var tokenValidationParameter = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(key),
    RequireExpirationTime = true,
    ClockSkew = TimeSpan.Zero
};

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3001", "http://localhost:3001", "https://localhost:3000", "https://localhost:3002", "https://pay.payos.vn")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
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
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new { message = "Authentication failed." });
            return context.Response.WriteAsync(result);
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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Chuyển hướng HTTPS

app.UseStaticFiles(); // Sử dụng các file tĩnh nếu có

app.UseRouting(); // Đảm bảo gọi UseRouting trước các middleware khác

app.UseCors("AllowReactApp"); // Enable CORS

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization(); // Enable authorization middleware

using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<GOStudyContext>();
    context.Database.Migrate();
}

using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<GOStudyContext>();
    context.Database.Migrate();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map các controller
    endpoints.MapHub<UserStatusHub>("/userStatusHub"); // Route cho SignalR Hub
});

app.Run();
