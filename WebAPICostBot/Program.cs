using Application;
using Application.IServiсe;
using Application.Service;
using DataAccess;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPICostBot;

var builder = WebApplication.CreateBuilder(args);

// Controllers 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "CostsBot API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token with Bearer prefix",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("ApplicationDb");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

//
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IExpenseReminderRepository, ExpenseReminderRepository>();
builder.Services.AddTransient<IPotentialPurchaseRepository, PotentialPurchaseRepository>();
builder.Services.AddTransient<IPotentialPurchaseService, PotentialPurchaseService>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
//
// Реєстрація репозиторіїв
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IDishProductRepository, DishProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDayMenuRepository, DayMenuRepository>();

//IProductService
builder.Services.AddScoped<IDishProductService, DishProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IDayMenuService, DayMenuService>();

builder.Services.AddTransient<IPeriodicReminderRepository, PeriodicReminderRepository>();
builder.Services.AddScoped<IPeriodicReminderService, PeriodicReminderService>();
//робить назви класів унікальними
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
});
// JWT
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });


builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();





