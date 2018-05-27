using Biblioteca.Application.Features.Books;
using Biblioteca.Application.Features.Loans;
using Biblioteca.Common.Tests.Features.ObjectMothers;
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
        public void Book_TesteService_AddBook_SouldBeOk()
        {
            _mockBookRepository.Setup(br => br.Save(_book)).Returns(_book);

            Book bookSaved = _bookService.Add(_book);

            bookSaved.Id.Should().Be(_book.Id);
            _mockBookRepository.Verify(br => br.Save(_book));
        }
    }
}
