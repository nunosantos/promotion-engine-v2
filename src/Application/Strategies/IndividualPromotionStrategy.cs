using Application.Interfaces;
using Domain.Orders;
using System;
using System.Linq;

namespace Application.Strategies
{
    public class IndividualPromotionStrategy : IPromotionStrategy
    {
        private readonly Promotion promotion;
        private readonly Order order;
        private readonly string skuId;
        private readonly Products products;

        public IndividualPromotionStrategy(Promotion promotion, Order order, string skuId, Products products)
        {
            this.promotion = promotion;
            this.order = order;
            this.skuId = skuId;
            this.products = products;
        }

        public int CalculateTotal()
        {
            if (promotion is null)
            {
                throw new ArgumentNullException(nameof(promotion));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var matchedOrderItem = order.OrderItems.FirstOrDefault(i => i.Id == skuId);

            var matchedProduct = products.ProductList.FirstOrDefault(p => p.Id == skuId);

            if (matchedOrderItem?.OrderedAmount < promotion.PriceTrigger && matchedProduct != null) return matchedOrderItem.OrderedAmount * matchedProduct.UnitPrice;
            {
                var total = 0;

                var numberOfItemsYetToBeCalculated = 0;

                for (var i = 0; i <= matchedOrderItem?.OrderedAmount; i++)
                {
                    if (i % promotion.PriceTrigger != 0 || i == 0) continue;

                    total += promotion.DiscountedPrice;

                    numberOfItemsYetToBeCalculated = matchedOrderItem.OrderedAmount - i;
                }

                if (matchedProduct != null)
                    return total + matchedProduct.UnitPrice * numberOfItemsYetToBeCalculated;
            }

            return 0;
        }
    }
}