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
        public virtual string Name { get; set; }

        public virtual double CostPrice { get; set; }

        public virtual double SalePrice { get; set; }

        public virtual DateTime Manufacture { get; set; }

        public virtual DateTime Expiration { get; set; }

        public virtual bool IsAvaliable { get; set; }

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
