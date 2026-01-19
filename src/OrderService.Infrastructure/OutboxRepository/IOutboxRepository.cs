using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.OutboxRepository
{
    public interface IOutboxRepository
    {
        Task SaveAsync(OutboxMessage order);
    }
}
