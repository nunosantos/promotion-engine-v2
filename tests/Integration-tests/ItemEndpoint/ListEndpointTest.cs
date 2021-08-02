using API;
using API.Endpoints.Items;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Integration_tests.ItemEndpoint
{
    public class ListEndpointTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public ListEndpointTest(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task ListAllItems()
        {
            var createOrderItemsCommand = new CreateItemCommand()
            {
                Items = new List<Product>
                {
                    new () { Id = "A", UnitPrice = 50},
                    new () { Id = "B", UnitPrice = 30},
                    new () { Id = "C", UnitPrice = 20},
                    new () { Id = "D", UnitPrice = 15},
                }
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/items", stringContent);

            response.EnsureSuccessStatusCode();

            var itemsResponse = await client.GetFromJsonAsync<ListItemResult>($"/items");

            itemsResponse.Items.Should().HaveCount(4);
            itemsResponse.Items[3].Id.Should().Be("D");
            itemsResponse.Items[3].UnitPrice.Should().Be(15);
        }
    }
}