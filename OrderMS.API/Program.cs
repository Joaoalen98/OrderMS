using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderMS.API.Application.Interfaces;
using OrderMS.API.Application.Services;
using OrderMS.API.Settings;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped(x =>
{
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    MongoClient mongoClient = new(settings!.ConnectionString);
    IMongoDatabase mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
    return mongoDatabase;
});

builder.Services.AddHostedService<RabbitMQService>();

var app = builder.Build();

app.MapGet("/customers/{customerId}/orders", async (IOrderService orderService, long customerId, [FromQuery] int page = 1, [FromQuery] int quantity = 10) =>
{
    return Results.Ok(await orderService.GetAllByCustomerId(customerId, page, quantity));
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.Run();
