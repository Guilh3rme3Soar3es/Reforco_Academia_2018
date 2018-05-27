using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookTitleNullOrEmptyException : BusinessException
    {
        public BookTitleNullOrEmptyException() : base("O livro deve conter um titulo.")
        {
        }
    }
}
