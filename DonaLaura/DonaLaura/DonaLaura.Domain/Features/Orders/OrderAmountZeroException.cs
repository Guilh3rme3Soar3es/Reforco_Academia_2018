using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public class OrderAmountZeroException : BusinessException
    {
        public OrderAmountZeroException() : base("A quantidade deve ser maior que zero.")
        {
        }
    }
}
