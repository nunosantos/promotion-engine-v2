using API.Endpoints.Orders;
using Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace API.Endpoints.Items
{
    public class RepositoryMapper
    {
        public static IEnumerable<Product> MapItem(CreateItemCommand request)
        {
            return request
                        .Items
                        .Select(i => new Product { Id = i.Id, UnitPrice = i.UnitPrice });
        }

        public static Order MapOrder(CreateOrderCommand request)
        {
            return new Order()
            {
                OrderItems = request.OrderItems
            };
        }
    }
}