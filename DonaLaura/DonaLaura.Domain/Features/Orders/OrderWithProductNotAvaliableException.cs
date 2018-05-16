using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public class OrderWithProductNotAvaliableException : BusinessException
    {
        public OrderWithProductNotAvaliableException() : base("Uma venda não pode conter produtos sem estoque disponivel.")
        {
        }
    }
}
