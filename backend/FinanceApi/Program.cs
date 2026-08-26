using FinanceApi.Data;
using FinanceApi.Services;
using Microsoft.EntityFrameworkCore;
using FinanceApi.Middleware;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDev", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=FinanceDb;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<FinanceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtService, JwtService>();

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key is missing.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowDev");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
