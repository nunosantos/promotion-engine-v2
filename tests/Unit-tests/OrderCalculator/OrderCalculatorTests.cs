using API;
using Application.Interfaces;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using Xunit;

namespace Unit_tests.OrderCalculator
{
    public class OrderCalculatorTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly IRepository _repository;

        public OrderCalculatorTests()
        {
            _repository = new Infrastructure.Repository();
        }

        [Fact]
        public void CreateOrder_ScenarioA_Buying_1xA_1xB_1xC_Returns100()
        {
            var products = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
                new() {Id = "C", UnitPrice = 20},
            };

            _repository.Add(products);

            var order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new()  { Id ="A",  OrderedAmount = 1},
                    new()  { Id ="B",  OrderedAmount = 1},
                    new()  { Id ="C",  OrderedAmount = 1 }
                }
            };

            var calculator = new Application.Services.OrderCalculatorService(_repository);

            var totalCalculatedCost = calculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(100);
        }

        [Fact]
        public void CreateOrder_ScenarioB_Buying_5xA_5xB_1xC_Returns370()
        {
            var products = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
                new() {Id = "C", UnitPrice = 20},
            };

            _repository.Add(products);

            var orderItems = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new() { Id ="A", OrderedAmount = 5},
                    new() { Id ="B", OrderedAmount = 5},
                    new() { Id ="C", OrderedAmount = 1}
                }
            };

            var calculator = new Application.Services.OrderCalculatorService(_repository);

            var totalCalculatedCost = calculator.CalculateItemTotal(orderItems);

            totalCalculatedCost.Should().Be(370);
        }

        [Fact]
        public void CreateOrder_ScenarioC_Buying_3xA_5xB_1xC_1xD_Returns280()
        {
            var products = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
                new() {Id = "C", UnitPrice = 20},
                new() {Id = "D", UnitPrice = 15},
            };

            _repository.Add(products);

            var order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new() { Id ="A", OrderedAmount = 3},
                    new() { Id ="B", OrderedAmount = 5},
                    new() { Id ="C", OrderedAmount = 1},
                    new() { Id ="D", OrderedAmount = 1}
                }
            };

            var calculator = new Application.Services.OrderCalculatorService(_repository);

            var totalCalculatedCost = calculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(280);
        }

        [Fact]
        public void CreateOrder_ScenarioD_AddNewPromotion_1xE_Returns300()
        {
            var products = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
                new() {Id = "C", UnitPrice = 20},
                new() {Id = "D", UnitPrice = 20},
                new() {Id = "E", UnitPrice = 20}
            };

            _repository.Add(products);

            var order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new() { Id ="A",  OrderedAmount = 3},
                    new() { Id ="B",  OrderedAmount = 5},
                    new() { Id ="C",  OrderedAmount = 1},
                    new() { Id ="D",  OrderedAmount = 1},
                    new() { Id ="E",  OrderedAmount = 1}
                }
            };

            var calculator = new Application.Services.OrderCalculatorService(_repository);

            var totalCalculatedCost = calculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(300);
        }

        [Fact]
        public void CreateOrder_ScenarioE_AddNewPromotion_1xE_4xF_Returns380()
        {
            var products = new List<Product>
            {
                new() {Id = "A", UnitPrice = 50},
                new() {Id = "B", UnitPrice = 30},
                new() {Id = "C", UnitPrice = 20},
                new() {Id = "D", UnitPrice = 20},
                new() {Id = "E", UnitPrice = 20},
                new() {Id = "F", UnitPrice = 40},
            };

            _repository.Add(products);

            var order = new Order()
            {
                OrderItems = new List<OrderItem>()
                {
                    new() { Id ="A",  OrderedAmount = 3},
                    new() { Id ="B",  OrderedAmount = 5},
                    new() { Id ="C",  OrderedAmount = 1},
                    new() { Id ="D",  OrderedAmount = 1},
                    new() { Id ="E",  OrderedAmount = 1},
                    new() { Id ="F",  OrderedAmount = 4},
                }
            };

            var calculator = new Application.Services.OrderCalculatorService(_repository);

            var totalCalculatedCost = calculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(460);
        }
    }
}