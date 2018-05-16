using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductNameShortException : BusinessException
    {
        public ProductNameShortException() : base("O nome deve ser maior que 4 caracteres")
        {
        }
    }
}
