namespace OrderService.Infrastructure.Messaging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Infrastructure.Event;

public class OutboxPublisher : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OutboxPublisher(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            var producer = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
            var messages = db.OutboxMessages.Where(x => !x.IsPublished).Take(10).ToList();

            foreach (var msg in messages)
            {
                await producer.PublishAsync("order-created", msg.Payload);
                msg.IsPublished = true;
            }

            await db.SaveChangesAsync();
            await Task.Delay(3000, stoppingToken);
        }
    }
}
