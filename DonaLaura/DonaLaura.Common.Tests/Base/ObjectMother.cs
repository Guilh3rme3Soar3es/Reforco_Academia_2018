using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Common.Tests.Base
{
    public static partial class ObjectMother
    {
        public static Product GetProductOk()
        {
            return new Product
            {
                Id = 2,
                Name = "Produto Ok",
                CostPrice = 2.50,
                SalePrice = 3.50,
                Manufacture = DateTime.Now.AddDays(-1),
                Expiration = DateTime.Now.AddDays(+1),
                IsAvaliable = true
            };
        }
    }
}
