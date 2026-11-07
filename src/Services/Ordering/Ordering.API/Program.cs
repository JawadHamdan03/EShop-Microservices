using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services
    .AddApiService(builder.Configuration)
    .AddInfrastructureService(builder.Configuration)
    .AddApplicationService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

