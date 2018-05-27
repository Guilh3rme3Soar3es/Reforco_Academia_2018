using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookAuthorNullOrEmptyException : BusinessException
    {
        public BookAuthorNullOrEmptyException() : base("O livro de conter um autor.")
        {
        }
    }
}
