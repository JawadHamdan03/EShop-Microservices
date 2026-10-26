using Basket.API.Baskets.DeleteBasket;
using Basket.API.Baskets.GetBasket;
using Basket.API.Baskets.StoreBasket;
using Basket.API.Data;
using Basket.API.Entities;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using JasperFx;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(confg =>
{
    confg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    confg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    confg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
    cfg.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository,BasketRepository>();

builder.Services.AddCarter(null, config =>
{
    config.WithModule<GetBasketEndpoint>();
    config.WithModule<StoreBasketEndpoint>();
    config.WithModule<DeleteBasketEndpoints>();
});


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapCarter();
app.UseExceptionHandler();
app.MapGet("/", () => "Hello World!");

app.Run();