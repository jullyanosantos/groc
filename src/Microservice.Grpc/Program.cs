using Microservices.Application;
using Microservices.GrpcServices;
using Microservices.Infrastructure;
using Microservices.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

//builder.WebHost.ConfigureKestrel((context, options) =>
//{
//    options.Listen(IPAddress.Any, 5000, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http1;
//    });
//    options.Listen(IPAddress.Any, 50051, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http2;
//    });
//});

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

Configure(app);

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ProductService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

void Configure(WebApplication host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            DbInitializer.Initialize(services, context);
        }
        catch (Exception ex)
        {

        }
    }
}