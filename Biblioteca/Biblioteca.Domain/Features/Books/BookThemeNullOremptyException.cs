using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookThemeNullOremptyException : BusinessException
    {
        public BookThemeNullOremptyException() : base("O livro deve conter uma tema.")
        {
        }
    }
}
