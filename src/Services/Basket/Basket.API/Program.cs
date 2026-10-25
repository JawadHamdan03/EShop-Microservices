

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(confg =>
{
    confg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();