using Domain.Orders;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Domain_tests
{
    public class DomainItemTest
    {
        [Fact]
        public void CreatesOrder_WhenSKUIDIsA_Returns50()
        {
            var orderItems = new Product() { Id = "A", UnitPrice = 50 };

            orderItems.Id.Should().Be("A");
            orderItems.UnitPrice.Should().Be(50);
        }

        [Fact]
        public void CreatesOrder_WhenSKUIDIsB_Returns50()
        {
            var orderItems = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
            };

            orderItems.Should().HaveCount(2);
            orderItems[1].Id.Should().Be("B");
            orderItems[1].UnitPrice.Should().Be(30);
        }

        [Fact]
        public void CreatesOrder_WhenSKUIDIsC_Returns20()
        {
            var orderItems = new List<Product>()
            {
                new() { Id = "A", UnitPrice = 50},
                new() { Id = "B", UnitPrice = 30},
                new() { Id = "C", UnitPrice = 20}
            };

            orderItems.Should().HaveCount(3);
            orderItems[2].Id.Should().Be("C");
            orderItems[2].UnitPrice.Should().Be(20);
        }

        [Fact]
        public void CreatesOrder_WhenSKUIDIsD_Returns15()
        {
            var orderItems = new List<Product>()
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
                new() {Id = "C", UnitPrice = 20},
                new() {Id = "D", UnitPrice = 15}
            };

            orderItems.Should().HaveCount(4);
            orderItems[3].Id.Should().Be("D");
            orderItems[3].UnitPrice.Should().Be(15);
        }
    }
}