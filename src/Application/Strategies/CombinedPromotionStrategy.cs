using Application.Interfaces;
using Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Application.Strategies
{
    public class CombinedPromotionStrategy : IPromotionStrategy
    {
        private readonly Promotion promotion;
        private readonly Order order;
        private readonly IEnumerable<Product> products;

        public CombinedPromotionStrategy(Promotion promotion, Order order, IEnumerable<Product> products)
        {
            this.promotion = promotion;
            this.order = order;
            this.products = products;
        }

        public int CalculateTotal()
        {
            if (promotion.Active) return 0;

            var applicableIDs = promotion.ApplicableIDs;

            var orderItems = order.OrderItems.Where(i => applicableIDs.Contains(i.Id)).ToList();

            var minimumOrderItemAmount = orderItems.Min(o => o.OrderedAmount);

            var maximumOrderItemAmount = orderItems.Max(o => o.OrderedAmount);

            var highestOrderedItem = orderItems.Aggregate((previous, next) =>
                previous.OrderedAmount > next.OrderedAmount ? previous : next);

            var matchedProduct = products.FirstOrDefault(p => p.Id == highestOrderedItem.Id);

            var orderContainsMultipleItems = orderItems.Count > 1;
            if (orderContainsMultipleItems)
            {
                var itemsHaveEquallyOrderedAmounts = maximumOrderItemAmount == minimumOrderItemAmount;
                if (itemsHaveEquallyOrderedAmounts)
                {
                    promotion.Active = true;
                    return promotion.DiscountedPrice * minimumOrderItemAmount;
                }

                return (promotion.DiscountedPrice * minimumOrderItemAmount) +
                       matchedProduct.UnitPrice * (maximumOrderItemAmount - minimumOrderItemAmount);
            }

            if (matchedProduct != null)
                return highestOrderedItem.OrderedAmount * matchedProduct.UnitPrice;

            return 0;
        }
    }
}