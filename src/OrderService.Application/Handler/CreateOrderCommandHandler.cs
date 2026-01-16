using OrderService.Application.Command;
using OrderService.Application.Event;
using OrderService.Application.Repository;
using OrderService.Application.Service;
using OrderService.Domain.Entity;
using OrderService.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Handler
{
    public class CreateOrderCommandHandler
    {
        private readonly IOrderService _orderService;
        private readonly IEventPublisher _eventPublisher;

        public CreateOrderCommandHandler(
            IOrderService orderService,
            IEventPublisher eventPublisher)
        {
            _orderService = orderService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(CreateOrderCommand command)
        {
            var order = new Order(command.ProductName, command.Amount);

            await _orderService.SaveAsync(order);

            var evt = new OrderCreatedEvent
            {
                OrderId = order.Id,
                ProductName = order.ProductName,
                Amount = order.Amount,
                CreatedAt = order.CreatedAt
            };

            await _eventPublisher.PublishAsync("order-created", evt);

            return order.Id;
        }
    }
}
