using OrderService.Application.Repository;
using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task SaveAsync(Order order)
        {
            try
            {
                await _orderRepository.SaveAsync(order);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
