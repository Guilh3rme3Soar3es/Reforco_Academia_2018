using DonaLaura.Domain.Features.Orders;
using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Tests.Features.Orders
{
    public static partial class ObjectMother
    {
        public static Order GetOrder(Product product)
        {
            return new Order
            {
                Id = 2,
                Client = "José da Silva",
                product = product,
                Amount = 1,
                Profit = 100.00,
            };
        }
    }
}
