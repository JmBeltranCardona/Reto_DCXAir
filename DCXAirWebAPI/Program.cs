using Application.Contracts.Algorithm.Search;
using Application.Contracts.ConversionCurrency;
using Application.Contracts.Journey;
using Application.Contracts.JsonData;
using Application.Cqrs.Journey.Queries;
using Application.Mapper;
using Application.Services.Algorithm.Search;
using Application.Services.ConversionCurrency;
using Application.Services.Journey;
using Domain.Contracts;
using Infrastructure.Context;
using Infrastructure.JsonData.Logic;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddDbContext<AplicationDBContext>(options => {
    options.UseSqlite(Configuration.GetConnectionString("ApplicationDB"));
    //options.UseSqlServer(connectionString.DecryptConnection());
});

builder.Services.AddHttpClient<ConversionCurrencyService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetRouteQuery).Assembly));
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
AutoMapperConfiguration.RegisterMappings();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//Dependency injection
builder.Services.AddScoped<IJsonData, JsonData>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IDfsRouteSearch, DfsRouteSearch>();
builder.Services.AddScoped<IConversionCurrencyService, ConversionCurrencyService>();


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
