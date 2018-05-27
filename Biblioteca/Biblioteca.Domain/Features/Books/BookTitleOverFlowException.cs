using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookTitleOverFlowException : BusinessException
    {
        public BookTitleOverFlowException() : base("O livro deve conter um titulo com no maximo 100 caracteres.")
        {
        }
    }
}
