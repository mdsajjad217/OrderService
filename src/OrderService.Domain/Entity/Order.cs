using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entity
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
