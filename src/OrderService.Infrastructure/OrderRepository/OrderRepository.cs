using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<List<Order>> GetAsync()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
