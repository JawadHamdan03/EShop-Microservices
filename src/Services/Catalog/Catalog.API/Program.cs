using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Catalog.API.Data;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;
using FluentValidation;
using HealthChecks.UI.Client;
using JasperFx;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
    cfg.AutoCreateSchemaObjects=AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
    options.AddOpenBehavior(typeof(ValidationBehavior<,>));
    options.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId",context.HttpContext.TraceIdentifier);
        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId",activity?.Id);
    };
});
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("database")!);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapCarter();
app.UseExceptionHandler();

app.UseHealthChecks("/health",
    new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
    );

app.Run();