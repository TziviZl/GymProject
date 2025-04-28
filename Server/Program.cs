using BL;
using BL.Api;
using DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DB_Manager>();
builder.Services.AddSingleton<DAL.Api.ITrainerDal, DAL.Services.TrainerDal>();
//builder.Services.AddSingleton<BL.Api.ITrainerBL, BL.Services.TrainerBL>();
builder.Services.AddSingleton<IBL,BlManager>();
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
