using Application.Interfaces;
using Domain.Orders;
using System.Linq;

namespace Application.Strategies
{
    public class PromotionNotActiveStrategy : IPromotionStrategy
    {
        private readonly Order order;
        private readonly string skuId;
        private readonly Products products;

        public PromotionNotActiveStrategy(Order order, string skuId, Products products)
        {
            this.order = order;
            this.skuId = skuId;
            this.products = products;
        }

        public int CalculateTotal()
        {
            var product = products.ProductList.FirstOrDefault(i => i.Id == skuId);

            var orderItem = order.OrderItems.FirstOrDefault(i => i.Id == skuId);

            if (product == null ^ orderItem == null)
                return 0;

            return orderItem.OrderedAmount * product.UnitPrice;
        }
    }
}