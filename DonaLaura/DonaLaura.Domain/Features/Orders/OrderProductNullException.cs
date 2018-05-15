using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public class OrderProductNullException : BusinessException
    {
        public OrderProductNullException() : base("A venda deve possuir um produto")
        {
        }
    }
}
