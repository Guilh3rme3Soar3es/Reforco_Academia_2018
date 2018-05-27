using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Loans
{
    public class LoanNoBookException : BusinessException
    {
        public LoanNoBookException() : base("O emprestimo deve conter um livro.")
        {
        }
    }
}
