using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.OutboxRepository
{
    public class OutboxRepository : IOutboxRepository
    {
        OrderDbContext _context;

        public OutboxRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(OutboxMessage order)
        {
            await _context.OutboxMessages.AddAsync(order);
        }
    }
}
