using BL;
using BL.Api;
using BL.Mapping;
using BL.Services;
using DAL.Api;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var relativeDbPath = Path.Combine("..", "..", "..", "..", "DAL", "data", "GymDB.mdf");
var fullDbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeDbPath));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
    .Replace("PATH_TO_REPLACE", fullDbPath);

// הגדרת DbContext עם הנתיב המלא
builder.Services.AddDbContext<DB_Manager>(options =>
    options.UseSqlServer(connectionString));

// DAL dependencies - Scoped
builder.Services.AddScoped<IGymnastDal, GymnastDal>();
builder.Services.AddScoped<ITrainerDal, TrainerDal>();

// BL dependencies - Scoped
builder.Services.AddScoped<IGymnastBL, GymnastBL>();
builder.Services.AddScoped<ITrainerBL, TrainerBL>();
builder.Services.AddScoped<IBL, BlManager>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Controllers & Swagger
//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();