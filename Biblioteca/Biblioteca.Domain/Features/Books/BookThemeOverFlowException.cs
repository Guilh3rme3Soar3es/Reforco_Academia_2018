using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookThemeOverFlowException : BusinessException
    {
        public BookThemeOverFlowException() : base("O livro deve conter um tema com no maximo 100 caracteres.")
        {
        }
    }
}
