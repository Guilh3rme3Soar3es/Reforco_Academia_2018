using Biblioteca.Application.Features.Books;
using Biblioteca.Application.Features.Loans;
using Biblioteca.Common.Tests.Features.ObjectMothers;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Books;
using Biblioteca.Domain.Features.Loans;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Tests.Features.Books
{
    [TestFixture]
    public class BookApplicationTests
    {
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<ILoanService> _mockLoanService;
        private BookService _bookService;

        private Book _book;
        private Loan _loan;

        [SetUp]
        public void Initialize()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _mockLoanService = new Mock<ILoanService>();
            _bookService = new BookService(_mockBookRepository.Object, _mockLoanService.Object);

            _book = ObjectMother.GetBookOK();
            _loan = ObjectMother.GetLoanOk(_book);
        }

        [Test]
        public void Book_TesteService_AddBook_SouldBeOk()
        {
            _mockBookRepository.Setup(br => br.Save(_book)).Returns(_book);

            Book bookSaved = _bookService.Add(_book);

            bookSaved.Id.Should().Be(_book.Id);
            _mockBookRepository.Verify(br => br.Save(_book));
        }

        [Test]
        public void Book_TesteService_AddBook_InvalidBook_SouldBeFail()
        {
            _book.Author = "";
            _mockBookRepository.Setup(br => br.Save(_book)).Returns(_book);

            Action comparation = () => _bookService.Add(_book);

            comparation.Should().Throw<BookAuthorNullOrEmptyException>();
            _mockBookRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Book_TesteService_UpdateBook_SouldBeOk()
        {
            _mockBookRepository.Setup(br => br.Update(_book)).Returns(_book);

            Book bookUpdated = _bookService.Update(_book);

            bookUpdated.Id.Should().Be(_book.Id);
            _mockBookRepository.Verify(br => br.Update(_book));
        }

        [Test]
        public void Book_TesteService_UpdateBook_InvalidId_SouldBeFail()
        {
            _book.Id = -1;
            _mockBookRepository.Setup(br => br.Update(_book)).Returns(_book);

            Action comparation = () => _bookService.Update(_book);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockBookRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Book_TesteService_UpdateBook_InvalidBook_SouldBeFail()
        {
            _book.Theme = "";
            _mockBookRepository.Setup(br => br.Update(_book)).Returns(_book);

            Action comparation = () => _bookService.Update(_book);

            comparation.Should().Throw<BookThemeNullOremptyException>();
            _mockBookRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Book_TesteService_GetBook_SouldBeOk()
        {
            _mockBookRepository.Setup(br => br.Get(_book.Id)).Returns(_book);

            Book bookFound = _bookService.Get(_book.Id);

            bookFound.Should().NotBeNull();
            _mockBookRepository.Verify(br => br.Get(_book.Id));
        }

        [Test]
        public void Book_TesteService_GetBook_InvalidId_SouldBeFail()
        {
            _book.Id = 0;
            _mockBookRepository.Setup(br => br.Get(_book.Id)).Returns(_book);

            Action comparation = () => _bookService.Get(_book.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockBookRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Book_TesteService_GetAll_SouldBeOk()
        {
            _mockBookRepository.Setup(br => br.GetAll()).Returns(new List<Book> { _book });

            var  lsitBook = _bookService.GetAll();

            lsitBook.Should().NotBeNullOrEmpty();
            _mockBookRepository.Verify(br => br.GetAll());
        }

        [Test]
        public void Book_TesteService_DeleteBook_SouldBeOk()
        {
            _mockBookRepository.Setup(br => br.Delete(_book));

            _bookService.Delete(_book);

            _mockBookRepository.Verify(br => br.Delete(_book));
        }

        [Test]
        public void Book_TesteService_DeleteBook_InvalidId_SouldBeFail()
        {
            _book.Id = 0;
            _mockBookRepository.Setup(br => br.Delete(_book));

            Action comparation = () =>_bookService.Delete(_book);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockBookRepository.VerifyNoOtherCalls();
            _mockLoanService.VerifyNoOtherCalls();
        }

        [Test]
        public void Book_TesteService_DeleteBook_BookWithRelatedLoan_SouldBeFail()
        {
            _mockBookRepository.Setup(br => br.Delete(_book));
            _mockLoanService.Setup(ls => ls.GetByBook(_book.Id)).Returns(new List<Loan> { _loan });

            Action comparation = () => _bookService.Delete(_book);

            comparation.Should().Throw<BookWithRelatedLoanException>();
            _mockLoanService.Verify(lc => lc.GetByBook(_book.Id));
            _mockBookRepository.VerifyNoOtherCalls();
        }
    }
}
