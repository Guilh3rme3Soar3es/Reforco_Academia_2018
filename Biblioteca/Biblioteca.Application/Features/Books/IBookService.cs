using Biblioteca.Domain.Features.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Features.Books
{
    public interface IBookService
    {
        Book Add(Book book);
        Book Update(Book book);
        Book Get(long id);
        IEnumerable<Book> GetAll();
        void Delete(Book book);
    }
}
