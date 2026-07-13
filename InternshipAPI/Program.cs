using InternshipAPI.Data;
using InternshipAPI.Interfaces;
using InternshipAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddSingleton<RabbitMqService>();

// Yeni servisler
builder.Services.AddSingleton<IConnector, ConnectorService>();
builder.Services.AddSingleton<RabbitMqAdapter>();
builder.Services.AddSingleton<NotificationProcessor>();
builder.Services.AddHostedService<ConnectorHostedService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();