using DonaLaura.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public class OrderClientNullOrEmptyException : BusinessException
    {
        public OrderClientNullOrEmptyException() : base("O nome do cliente não deve ser vazio")
        {
        }
    }
}
