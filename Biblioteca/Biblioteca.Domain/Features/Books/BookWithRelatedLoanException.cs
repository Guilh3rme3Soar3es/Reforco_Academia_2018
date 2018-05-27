using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookWithRelatedLoanException : BusinessException
    {
        public BookWithRelatedLoanException() : base("O livro não pode ser excluido pois esta relacionado a um emprestimo.")
        {
        }
    }
}
