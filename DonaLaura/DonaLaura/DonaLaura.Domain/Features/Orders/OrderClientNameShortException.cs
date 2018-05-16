using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public class OrderClientNameShortException : BusinessException
    {
        public OrderClientNameShortException() : base("O nome do cliente deve ser maior que 3 caracteres")
        {
        }
    }
}
