using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookAuthorOverFlowException : BusinessException
    {
        public BookAuthorOverFlowException() : base("O nome do autor deve conter no maximo 100 caracteres.")
        {
        }
    }
}
