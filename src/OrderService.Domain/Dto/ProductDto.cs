using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
