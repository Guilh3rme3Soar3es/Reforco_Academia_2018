using Biblioteca.Application.Features.Books;
using Biblioteca.Application.Features.Loans;
using Biblioteca.Common.Tests.Base;
using Biblioteca.Common.Tests.Features.ObjectMothers;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Books;
using Biblioteca.Infra.Data.Features.Books;
using Biblioteca.Infra.Data.Features.Loans;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Integration.Tests.Features.Books
{
    [TestFixture]
    public class BookIntegrationSqlTests
    {
        private BookService _bookService;
        private LoanService _loanService;
        private BookRepository _bookRepository;
        private LoanRepository _loanRepositopty;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();

            _loanRepositopty = new LoanRepository();
            _bookRepository = new BookRepository();
            _loanService = new LoanService(_loanRepositopty);
            _bookService = new BookService(_bookRepository, _loanService);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_ShouldBeOk()
        {
            Book BookToSave = ObjectMother.GetBookOK();

            Book BookSaved = _bookService.Add(BookToSave);

            Book resultadoEncontrado = _bookService.Get(BookSaved.Id);
            BookSaved.Should().NotBeNull();
            resultadoEncontrado.Id.Should().Be(BookSaved.Id);
            BookSaved.Title.Should().Be(BookToSave.Title);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_TitleNullOrEmpty_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Title = null;

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookTitleNullOrEmptyException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_TitleShort_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Title = "ABC";

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookShortTitleException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_TitleOverFlow_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookTitleOverFlowException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_ThemeNullOrEmpty_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Theme = null;

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookThemeNullOremptyException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_ThemeShort_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Theme = "ABC";

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookShortThemeException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_ThemeOverFlow_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Theme = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookThemeOverFlowException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_NameAuthorNullOrEmpty_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Author = null;

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookAuthorNullOrEmptyException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_NameAuthorShort_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Author = "ABC";

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookShortAuthorException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_NameAuthorOverFlow_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Author = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookAuthorOverFlowException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_InvalidVolume_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.Volume = 0;

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookInvalidVolumeException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_SaveBook_InvalidPostDate_ShouldBeFail()
        {
            Book BookToSave = ObjectMother.GetBookOK();
            BookToSave.PostDate = DateTime.Now.AddDays(+1);

            Action comparation = () => _bookService.Add(BookToSave);

            comparation.Should().Throw<BookInvalidPostDateException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_ShouldBeOk()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();

            Book BookSaved = _bookService.Update(BookToUpdate);

            Book resultadoEncontrado = _bookService.Get(BookSaved.Id);
            BookSaved.Should().NotBeNull();
            resultadoEncontrado.Id.Should().Be(BookSaved.Id);
            BookSaved.Title.Should().Be(BookToUpdate.Title);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_InvalidId_ShouldBeOk()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Id = 0;

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }


        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_TitleNullOrEmpty_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Title = null;

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookTitleNullOrEmptyException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_TitleShort_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Title = "ABC";

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookShortTitleException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_TitleOverFlow_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookTitleOverFlowException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_ThemeNullOrEmpty_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Theme = null;

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookThemeNullOremptyException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_ThemeShort_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Theme = "ABC";

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookShortThemeException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_ThemeOverFlow_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Theme = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookThemeOverFlowException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_NameAuthorNullOrEmpty_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Author = null;

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookAuthorNullOrEmptyException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_NameAuthorShort_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Author = "ABC";

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookShortAuthorException>();
            var booksFound = _bookService.GetAll();
            booksFound.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_NameAuthorOverFlow_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Author = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookAuthorOverFlowException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_InvalidVolume_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.Volume = 0;

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookInvalidVolumeException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_UpdateBook_InvalidPostDate_ShouldBeFail()
        {
            Book BookToUpdate = ObjectMother.GetBookOK();
            BookToUpdate.PostDate = DateTime.Now.AddDays(+1);

            Action comparation = () => _bookService.Update(BookToUpdate);

            comparation.Should().Throw<BookInvalidPostDateException>();
            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_DeleteBook__ShouldBeOk()
        {
            BaseSqlTests.ClearTBLoan();
            Book BookToDelete = ObjectMother.GetBookOK();

            _bookService.Delete(BookToDelete);

            var BookRetorns = _bookService.GetAll();
            BookRetorns.Count().Should().Be(0);
        } 
        

        [Test]
        public void Book_TestSystemIntegrationSql_DeleteBook_InvalidId_ShouldBeFail()
        {
            Book BookToDelete = ObjectMother.GetBookOK();
            BookToDelete.Id = -1;

            Action comparation = () => _bookService.Delete(BookToDelete);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var listPosts = _bookService.GetAll();
            listPosts.Count().Should().Be(1);
            listPosts.First().Id.Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_GetAll__ShouldBeOk()
        {
            var listBooks = _bookService.GetAll();

            listBooks.Should().NotBeNullOrEmpty();
            listBooks.Count().Should().Be(1);
            listBooks.First().Id.Should().Be(1);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_GetAll__ShouldBeNull()
        {
            BaseSqlTests.ClearDataBase();

            var listBooks = _bookService.GetAll();

            listBooks.Should().BeNullOrEmpty();
        }

        [Test]
        public void Book_TestSystemIntegrationSql_GetBook__ShouldBeOk()
        {
            var id = 1;
            Book BookFound = _bookService.Get(id);

            BookFound.Should().NotBeNull();
            BookFound.Id.Should().Be(id);
        }

        [Test]
        public void Book_TestSystemIntegrationSql_GetBook_InvalidId_ShouldBeFail()
        {
            var id = -1;
            Action comparation = () => _bookService.Get(id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Book_TestSystemIntegrationSql_GetBook__ShouldBeNull()
        {
            var id = 2;
            Book BookFound = _bookService.Get(id);

            BookFound.Should().BeNull();
        }

        [Test]
        public void Book_TestSystemIntegrationSql_GetByBook_BookWithDependences_ShouldBeFail()
        {
            Book Book = ObjectMother.GetBookOK();
            Book.Id = 1;
 
            Action comparation = () => _bookService.Delete(Book);

            comparation.Should().Throw<BookWithRelatedLoanException>();
            var BookFound = _bookService.GetAll();
            BookFound.Count().Should().Be(1);
        }
    }
}
