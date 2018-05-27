using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class BookInvalidVolumeException : BusinessException
    {
        public BookInvalidVolumeException() : base("O livro deve conter um volume maior que 0.")
        {
        }
    }
}
