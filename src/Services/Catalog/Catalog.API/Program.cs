using Carter;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;
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
builder.Services.AddCarter(null, configurator =>
{
    configurator.WithModule<CreateProductEndpoint>();
    configurator.WithModule<GetProductsEndpoint>();
    configurator.WithModule<GetProductByIdEndpoint>();
    configurator.WithModule<GetProductsByCategoryEndpoint>();
    configurator.WithModule<UpdateProductEndpoint>();
    configurator.WithModule<DeleteProductEndpoint>();

});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();