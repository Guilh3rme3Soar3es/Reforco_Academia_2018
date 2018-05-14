using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductNameOverFlowException : BusinessException
    {
        public ProductNameOverFlowException() : base("O nome deve ter no maximo 100 caracteres")
        {
        }
    }
}
