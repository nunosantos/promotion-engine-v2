using API;
using API.Endpoints.Items;
using API.Endpoints.Orders;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Integration_tests.OrderEndpoint
{
    public class CreateEndpoint : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public CreateEndpoint(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder()
        {
            var createItemsCommand = new CreateItemCommand()
            {
                Items = new List<Product>
                {
                    new () { Id = "A", UnitPrice = 50},
                    new () { Id = "B", UnitPrice = 30},
                    new () { Id = "C", UnitPrice = 20},
                    new () { Id = "D", UnitPrice = 15},
                }
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(createItemsCommand), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/items", stringContent);

            response.EnsureSuccessStatusCode();

            var createOrderItemsCommand = new CreateOrderCommand()
            {
                OrderItems = Enumerable.Repeat(new OrderItem() { Id = "A", OrderedAmount = 3 }, 1)
            };

            stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");

            response = await client.PostAsync($"/order", stringContent);

            var stringResponse = await response.Content.ReadAsStringAsync();

            var order = JsonConvert.DeserializeObject<Order>(stringResponse);

            order.Total.Should().Be(130);
        }
    }
}