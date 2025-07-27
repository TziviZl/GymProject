using BL;
using BL.Api;
using BL.Mapping;
using BL.Services;
using DAL.Api;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
using Server.Middleware;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var relativeDbPath = Path.Combine("..", "..", "..", "..", "DAL", "data", "GymDB.mdf");
var fullDbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeDbPath));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
    .Replace("PATH_TO_REPLACE", fullDbPath);

builder.Services.AddDbContext<DB_Manager>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGymnastDal, GymnastDal>();
builder.Services.AddScoped<ITrainerDal, TrainerDal>();
builder.Services.AddScoped<IStudioClassDal, StudioClassDal>();
builder.Services.AddScoped<IUserTypeDal, UserTypeDal>();
builder.Services.AddScoped<IMessageDal, MessageDal>();


builder.Services.AddScoped<IGymnastBL, GymnastBL>();
builder.Services.AddScoped<ITrainerBL, TrainerBL>();
builder.Services.AddScoped<IStudioClassBL, StudioClassBL>();
builder.Services.AddScoped<IUserTypeBL, UserTypeBL>();
builder.Services.AddScoped<IMessageBL, MessageBL>();
builder.Services.AddScoped<IBL, BlManager>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<StudioClassResetService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<StudioClassResetService>());


// CORS - הגדרת מדיניות שתאפשר גישה מכתובת ה־React שלך
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // <-- כתובת ה־React בזמן פיתוח
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Controllers & Swagger
//builder.Services.AddControllers();
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
app.UseCors(MyAllowSpecificOrigins);

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();