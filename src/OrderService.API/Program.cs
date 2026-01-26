using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.API.Client;
using OrderService.API.Option;
using OrderService.Application.Handler;
using OrderService.Application.Service;
using OrderService.Domain.Option;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Event;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.OrderRepository;
using OrderService.Infrastructure.OutboxRepository;
using OrderService.Infrastructure.Producer;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));

builder.Services.Configure<ApiClients>(builder.Configuration.GetSection(nameof(ApiClients)));

var apliClient = builder.Configuration.GetSection(nameof(ApiClients)).Get<ApiClients>();

builder.Services.Configure<KafkaProducerOptions>(
    builder.Configuration.GetSection("Kafka")
);

builder.Services.AddScoped<CreateOrderCommandHandler, CreateOrderCommandHandler>();
builder.Services.AddScoped<GetOrderQueryHandler, GetOrderQueryHandler>();
builder.Services.AddScoped<IOrderService, OrderingService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();

builder.Services.AddHttpClient<ProductApiClient>(client =>
{
    client.BaseAddress = new Uri(apliClient.ProductServiceBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(3);
});

builder.Services.AddScoped<IEventPublisher, KafkaProducer>();

builder.Services.AddHostedService<OutboxPublisher>();

//builder.Services.AddHttpClient<ProductApiClient>(client =>
//     client.AddPolicyHandler(retryPolicy)
//    .AddPolicyHandler(circuitBreakerPolicy)
//    );
builder.WebHost.UseUrls("http://0.0.0.0:8084");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();