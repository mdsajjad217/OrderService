using OrderService.Application.Command;
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

        public CreateOrderCommandHandler(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Guid> Handle(CreateOrderCommand command)
        {
            var order = new Order(command.ProductName, command.Amount);

            await _orderService.SaveAsync(order);            

            return order.Id;
        }
    }
}
