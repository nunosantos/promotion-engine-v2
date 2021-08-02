using Domain.Orders;
using FluentAssertions;
using Xunit;

namespace Domain_tests
{
    public class DomainPromotionTest
    {
        [Fact]
        public void CreatesPromotion_WhenSKUIDIsA_Returns50()
        {
            var orderItems = new Promotion()
            {
                Id = "A",
                DiscountedPrice = 30,
                PriceTrigger = 3
            };

            orderItems.Id.Should().Be("A");
            orderItems.DiscountedPrice.Should().Be(30);
            orderItems.PriceTrigger.Should().Be(3);
        }
    }
}