using OrderService.Application.Command;
using OrderService.Application.Query;
using OrderService.Application.Service;
using OrderService.Domain.Dto;
using OrderService.Domain.Entity;
using OrderService.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Handler
{
    public class GetOrderQueryHandler
    {
        private readonly IOrderService _orderService;

        public GetOrderQueryHandler(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<OrderDto>> Handle()
        {
            var orders = await _orderService.GetAsync();

            return orders.Select(x => new OrderDto(x.ProductName, x.Amount)).ToList();
        }
    }
}
