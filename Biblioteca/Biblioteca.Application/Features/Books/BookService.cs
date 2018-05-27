using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Application.Features.Loans;
using Biblioteca.Domain.Features.Books;

namespace Biblioteca.Application.Features.Books
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private ILoanService _loanService;

        public BookService(IBookRepository bookRepository, ILoanService loanService)
        {
            _bookRepository = bookRepository;
            _loanService = loanService;
        }

        public Book Add(Book book)
        {
        }

        public void Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public Book Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public Book Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
