using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Loans
{
    public class LoanClientNameNullOrEmptyException : BusinessException
    {
        public LoanClientNameNullOrEmptyException() : base("O emprestimo deve conter uma cliente valido.")
        {
        }
    }
}
