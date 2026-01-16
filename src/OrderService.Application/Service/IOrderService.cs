using OrderService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Service
{
    public interface IOrderService
    {
        Task SaveAsync(Order order);
    }
}
