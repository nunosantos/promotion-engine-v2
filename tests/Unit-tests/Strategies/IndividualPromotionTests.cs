using API;
using Application.Strategies;
using Application.Stubs;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Unit_tests.Strategies
{
    public class IndividualPromotionTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public void CalculateTotal_IfPromotionIsValid_ReturnPromotionPrice()
        {
            var orderItemId = "A";

            var promotion = Promotions.GetPromotions().FirstOrDefault(p => p.ApplicableIDs.Contains(orderItemId));

            var productItem = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50}
            };

            var products = new Products() { ProductList = productItem.ToArray() };

            var order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new() {Id = "A", OrderedAmount = 5},
                }
            };

            var strategy = new IndividualPromotionStrategy(promotion, order, orderItemId, products);
            var total = strategy.CalculateTotal();

            total.Should().Be(230);
        }
    }
}