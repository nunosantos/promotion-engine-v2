using Domain.Orders;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IRepository
    {
        void Add(IEnumerable<Product> item);

        IEnumerable<Product> Get();
    }
}