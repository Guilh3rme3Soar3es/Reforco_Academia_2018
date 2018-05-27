using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookShortThemeException : BusinessException
    {
        public BookShortThemeException() : base("O livro deve conter uma tema com no minimo 4 caracteres.")
        {
        }
    }
}
