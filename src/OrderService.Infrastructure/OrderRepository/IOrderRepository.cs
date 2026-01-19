using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.OrderRepository
{
    public interface IOrderRepository
    {
        Task SaveAsync(Domain.Entity.Order order);
    }
}
