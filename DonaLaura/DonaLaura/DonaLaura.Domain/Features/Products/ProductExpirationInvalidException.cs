using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductExpirationInvalidException : BusinessException
    {
        public ProductExpirationInvalidException() : base("A data de validade deve ser maior que a data de fabricação.")
        {
        }
    }
}
