using Domain.Orders;
using System.Collections.Generic;

namespace API.Endpoints.Items
{
    public class CreateItemCommand
    {
        public IEnumerable<Product> Items { get; set; }
    }
}