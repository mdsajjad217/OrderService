using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Dto
{
    public class OrderDto
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }

        public OrderDto(string productName, decimal amount)
        {
            ProductName = productName;
            Amount = amount;
        }
    }
}
