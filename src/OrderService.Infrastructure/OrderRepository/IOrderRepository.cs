using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.OrderRepository
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
        Task<List<Order>> GetAsync();
    }
}
