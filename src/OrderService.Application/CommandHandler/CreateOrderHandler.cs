using OrderService.Application.Event;
using OrderService.Domain.Entity;
using OrderService.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.CommandHandler
{
    public class CreateOrderHandler
    {
        private readonly IEventPublisher _producer;

        public async Task HandleAsync(Order order)
        {
            // Save order to DB first

            var evt = new OrderCreatedEvent
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                CreatedAt = DateTime.UtcNow
            };

            await _producer.PublishAsync("order-created", evt);
        }
    }
}
