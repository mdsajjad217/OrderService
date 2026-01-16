using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.API.Client;
using OrderService.API.Option;
using OrderService.Application.Event;
using OrderService.Application.Option;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Producer;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));

builder.Services.Configure<ApiClients>(builder.Configuration.GetSection(nameof(ApiClients)));

var apliClient = builder.Configuration.GetSection(nameof(ApiClients)).Get<ApiClients>();

builder.Services.Configure<KafkaProducerOptions>(
    builder.Configuration.GetSection("Kafka")
);

builder.Services.AddHttpClient<ProductApiClient>(client =>
{
    client.BaseAddress = new Uri(apliClient.ProductServiceBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(3);
});

builder.Services.AddScoped<IEventPublisher, KafkaProducer>();

//builder.Services.AddHttpClient<ProductApiClient>(client =>
//     client.AddPolicyHandler(retryPolicy)
//    .AddPolicyHandler(circuitBreakerPolicy)
//    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();