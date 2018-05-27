using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookInvalidPostDateException : BusinessException
    {
        public BookInvalidPostDateException() : base("O livro deve conter uma data de publicação igual ou menor que a date atual.")
        {
        }
    }
}
