using Carter;
using Catalog.API.Products.CreateProduct;
using JasperFx;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
    cfg.AutoCreateSchemaObjects=AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();


builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddOpenApi();
builder.Services.AddCarter(null,configurator =>
    configurator.WithModule<CreateProductEndpoint>());
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();