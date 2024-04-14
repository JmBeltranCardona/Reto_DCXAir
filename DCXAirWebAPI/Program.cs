using Infrastructure.Context;
using Infrastructure.JsonData.Contracts;
using Infrastructure.JsonData.Logic;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddDbContext<AplicationDBContext>(options => {
    options.UseSqlite(Configuration.GetConnectionString("ApplicationDB"));
    //options.UseSqlServer(connectionString.DecryptConnection());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddScoped<IJsonData, JsonData>();

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
