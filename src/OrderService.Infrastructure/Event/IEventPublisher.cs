namespace OrderService.Infrastructure.Event
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string topic, T message);
    }
}