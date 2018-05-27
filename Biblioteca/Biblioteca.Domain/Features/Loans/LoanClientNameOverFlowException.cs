using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Loans
{
    public class LoanClientNameOverFlowException : BusinessException
    {
        public LoanClientNameOverFlowException() : base("O emprestimo deve conter um cliente com nome valido.")
        {
        }
    }
}
