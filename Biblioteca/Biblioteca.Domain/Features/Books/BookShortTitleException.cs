using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookShortTitleException : BusinessException
    {
        public BookShortTitleException() : base("O livro deve conter um titulo com no minimo 4 caracteres.")
        {
        }
    }
}
