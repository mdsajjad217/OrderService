using OrderService.Domain.Entity;
using OrderService.Domain.Event;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Event;
using OrderService.Infrastructure.OrderRepository;
using OrderService.Infrastructure.OutboxRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderService.Application.Service
{
    public class OrderingService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOutboxRepository _outboxRepository;

        private readonly IEventPublisher _eventPublisher;
        private readonly OrderDbContext _context;

        public OrderingService(IOrderRepository orderRepository, IOutboxRepository outboxRepository,
            IEventPublisher eventPublisher, OrderDbContext context)
        {
            _orderRepository = orderRepository;
            _outboxRepository = outboxRepository;
            _eventPublisher = eventPublisher;
            _context = context;
        }

        public async Task SaveAsync(Order order)
        {
            try
            {
                JsonSerializerOptions JsonOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
                await _orderRepository.SaveAsync(order);

                var evt = new OrderCreatedEvent
                {
                    OrderId = order.Id,
                    ProductName = order.ProductName,
                    Amount = order.Amount,
                    CreatedAt = order.CreatedAt
                };

                await _outboxRepository.SaveAsync(new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = nameof(OrderCreatedEvent),
                    Payload = JsonSerializer.Serialize(evt, JsonOptions),
                    OccurredOn = DateTime.UtcNow,
                    IsPublished = false
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}