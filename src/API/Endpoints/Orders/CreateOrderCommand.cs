using Domain.Orders;
using System.Collections.Generic;

namespace API.Endpoints.Orders
{
    public class CreateOrderCommand
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}