namespace OrderService.Infrastructure.Messaging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Infrastructure.Event;

public class OutboxPublisher : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEventPublisher _producer;

    public OutboxPublisher(
        IServiceScopeFactory scopeFactory, IEventPublisher producer)
    {
        _scopeFactory = scopeFactory;
        _producer = producer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

            var messages = db.OutboxMessages.Where(x => !x.IsPublished).Take(10).ToList();

            foreach (var msg in messages)
            {
                await _producer.PublishAsync("order-created", msg.Payload);
                msg.IsPublished = true;
            }

            await db.SaveChangesAsync();
            await Task.Delay(3000, stoppingToken);
        }
    }
}
