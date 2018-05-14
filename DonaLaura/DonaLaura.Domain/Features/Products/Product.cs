using DonaLaura.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public double CostPrice { get; set; }

        public double SalePrice { get; set; }

        public DateTime Manufacture { get; set; }

        public DateTime Expiration { get; set; }

        public bool IsAvaliable { get; set; }

        public override void Validate()
        {
            if (String.IsNullOrEmpty(Name))
                throw new ProductNameNullOrEmptyException();
            if (Name.Length <= 4)
                throw new ProductNameShortException();
            if (Name.Length > 100)
                throw new ProductNameOverFlowException();
            if (CostPrice > SalePrice)
                throw new ProductCostPriceOverFlow();
            if (Expiration < Manufacture)
                throw new ProductExpirationInvalidException();
        }
    }
}
