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

            var orderItems = order.OrderItems
                                            .Where(i => applicableIDs.Contains(i.Id))
                                            .OrderBy(o => o.OrderedAmount)
                                            .ToList();

            var highestOrderedItem = orderItems.Aggregate((previous, next) =>
                previous.OrderedAmount > next.OrderedAmount ? previous : next);

            var matchedProduct = products.FirstOrDefault(p => p.Id == highestOrderedItem.Id);

            var orderContainsMultipleItems = orderItems.Count > 1;

            if (orderContainsMultipleItems)
                return SelectApplicablePromotionPrice(orderItems.First(), orderItems.Last(), matchedProduct);

            if (matchedProduct != null)
                return highestOrderedItem.OrderedAmount * matchedProduct.UnitPrice;

            return 0;
        }

        private int SelectApplicablePromotionPrice(OrderItem orderItemWithHighestAmount, OrderItem orderItemWithLowestAmount, Product? matchedProduct)
        {
            var itemsHaveEquallyOrderedAmounts = orderItemWithHighestAmount.OrderedAmount == orderItemWithLowestAmount.OrderedAmount;

            return itemsHaveEquallyOrderedAmounts
                ? CalculatePromotionPrice(orderItemWithLowestAmount)
                : CalculateDefaultPrice(orderItemWithLowestAmount, orderItemWithHighestAmount, matchedProduct);
        }


        private int CalculateDefaultPrice(OrderItem orderItemWithHighestAmount, OrderItem orderItemWithLowestAmount, Product? matchedProduct)
        {
            return (promotion.DiscountedPrice * orderItemWithLowestAmount.OrderedAmount) +
                   matchedProduct.UnitPrice * (orderItemWithHighestAmount.OrderedAmount - orderItemWithLowestAmount.OrderedAmount);
        }

        private int CalculatePromotionPrice(OrderItem orderItemWithLowestAmount)
        {
            promotion.Active = true;
            return promotion.DiscountedPrice * orderItemWithLowestAmount.OrderedAmount;
        }
    }
}