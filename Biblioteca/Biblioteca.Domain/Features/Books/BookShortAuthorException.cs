using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookShortAuthorException : BusinessException
    {
        public BookShortAuthorException() : base("O nome do autor deve conter ao menos 4 caracteres.")
        {
        }
    }
}
