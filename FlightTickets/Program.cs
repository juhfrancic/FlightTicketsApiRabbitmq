using FlightTickets.ConsumerAPI.Data;
using FlightTickets.ConsumerAPI.Repositories;
using FlightTickets.ConsumerAPI.Repositories.Interfaces;
using FlightTickets.ConsumerAPI.Services;
using FlightTickets.ConsumerAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB")
);

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddSingleton<IConsumerRepository, ConsumerRepository>();
builder.Services.AddSingleton<IConsumerService, ConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
