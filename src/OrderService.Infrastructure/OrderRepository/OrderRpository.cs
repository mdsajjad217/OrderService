using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.OrderRepository
{
    public class OrderRpository : IOrderRepository
    {
        OrderDbContext _context;

        public OrderRpository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Order order)
        {
            await _context.Orders.AddAsync(order);            
        }
    }
}
