using System.Collections.Generic;

namespace Domain.Orders
{
    public class Order
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public int Total { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }
        public int OrderedAmount { get; set; }
    }
}