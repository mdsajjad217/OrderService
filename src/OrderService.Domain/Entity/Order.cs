using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entity
{
    public class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string ProductName { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Order(string productName, decimal amount)
        {
            ProductName = productName;
            Amount = amount;
        }
    }
}
