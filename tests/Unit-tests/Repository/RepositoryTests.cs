using Application.Interfaces;
using Domain.Orders;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Unit_tests.Repository
{
    public class RepositoryTests
    {
        private readonly IRepository _repository;

        public RepositoryTests()
        {
            _repository = new Infrastructure.Repository();
        }

        [Fact]
        public void Add_Item_To_Repository()
        {
            var item = new List<Product>
            {
                new()
                {
                    Id = "A",
                    UnitPrice = 20
                }
            };
            _repository.Add(item);
            var items = _repository.Get();
            items.Should().HaveCount(1);
        }

        [Fact]
        public void Get_Items_From_Repository()
        {
            var items = new List<Product>()
            {
                new()
                {
                    Id = "A",
                    UnitPrice = 20
                },
                new()
                {
                    Id = "B",
                    UnitPrice = 20
                }
            };

            _repository.Add(items);

            var item = _repository.Get();
            item.Should().HaveCount(2);
        }
    }
}