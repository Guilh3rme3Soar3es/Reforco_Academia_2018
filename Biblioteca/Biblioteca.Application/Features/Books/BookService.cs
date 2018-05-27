using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Application.Features.Loans;
using Biblioteca.Domain.Exceptions;
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
            book.Validate();
            return _bookRepository.Save(book);
        }

        public void Delete(Book book)
        {
            if (book.Id <= 0)
                throw new IdentifierUndefinedException();
            if (_loanService.GetByBook(book.Id).Count() > 0)
                throw new BookWithRelatedLoanException();
            _bookRepository.Delete(book);
        }

        public Book Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _bookRepository.Get(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book Update(Book book)
        {
            if (book.Id <= 0)
                throw new IdentifierUndefinedException();
            book.Validate();
            return _bookRepository.Update(book);
        }
    }
}
