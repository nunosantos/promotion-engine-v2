using Application.Interfaces;
using Domain.Orders;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Repository : IRepository
    {
        private readonly List<Product> _items;

        public Repository()
        {
            _items = new List<Product>();
        }

        public void Add(IEnumerable<Product> items)
        {
            _items.AddRange(items);
        }

        public IEnumerable<Product> Get()
        {
            return _items;
        }
    }
}