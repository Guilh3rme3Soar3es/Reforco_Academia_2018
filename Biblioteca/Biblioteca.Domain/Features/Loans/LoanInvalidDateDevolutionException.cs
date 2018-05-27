using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Loans
{
    public class LoanInvalidDateDevolutionException : BusinessException
    {
        public LoanInvalidDateDevolutionException() : base("O emprestimo deve conter uma data de devolução maior que a date atual.")
        {
        }
    }
}
