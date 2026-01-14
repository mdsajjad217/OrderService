using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Event
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
