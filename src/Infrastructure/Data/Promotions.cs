using Domain.Orders;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public static class Promotions
    {
        public static IEnumerable<Promotion> GetPromotions()
        {
            yield return new Promotion()
            {
                PriceTrigger = 3,
                DiscountedPrice = 130,
                ApplicableIDs = new string[] { "A" },
            };
            yield return new Promotion()
            {
                PriceTrigger = 2,
                DiscountedPrice = 45,
                ApplicableIDs = new string[] { "B" }
            };
            yield return new Promotion()
            {
                DiscountedPrice = 30,
                Id = "CD",
                ApplicableIDs = new string[] { "C", "D" },
            };
        }
    }
}