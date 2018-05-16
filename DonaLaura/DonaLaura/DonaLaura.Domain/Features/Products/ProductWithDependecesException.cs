using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Products
{
    public class ProductWithDependecesException : BusinessException
    {
        public ProductWithDependecesException() : base("O produto não pode ser excluido, pois possui esta vinculado a uma venda.")
        {
        }
    }
}
