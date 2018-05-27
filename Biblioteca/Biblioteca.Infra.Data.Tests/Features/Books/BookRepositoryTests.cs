using Biblioteca.Common.Tests.Base;
using Biblioteca.Common.Tests.Features.ObjectMothers;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Books;
using Biblioteca.Infra.Data.Features.Books;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Tests.Features.Books
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private BookRepository _bookRepository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _bookRepository = new BookRepository();
        }

        [Test]
        public void Book_RepositoryTests_AddBook_ShouldBeOk()
        {
            int id = 2;
            Book bookToSave = ObjectMother.GetBookOK();

            Book bookSaved = _bookRepository.Save(bookToSave);

            bookSaved.Id.Should().Be(id);
            bookSaved.Title.Should().Be(bookToSave.Title);
        }

        [Test]
        public void Book_RepositoryTests_AddBook_InvalidBook_ShouldBeFail()
        {
            Book bookToSave = ObjectMother.GetBookOK();
            bookToSave.Title = "";

            Action comparation = () => _bookRepository.Save(bookToSave);

            comparation.Should().Throw<BookTitleNullOrEmptyException>();
        }

        [Test]
        public void Book_RepositoryTests_UpdateBook_ShouldBeOk()
        {
            Book bookToUpdate = ObjectMother.GetBookOK();
            bookToUpdate.Title = "Teste de atualização";

            Book bookUpdated = _bookRepository.Update(bookToUpdate);

            bookUpdated.Title.Should().Be(bookToUpdate.Title);
        }

        [Test]
        public void Book_RepositoryTests_UpdateBook_InvalidBook_ShouldBeFail()
        {
            Book bookToUpdate = ObjectMother.GetBookOK();
            bookToUpdate.Title = "";

            Action comparation = () => _bookRepository.Update(bookToUpdate);

            comparation.Should().Throw<BookTitleNullOrEmptyException>();
        }

        [Test]
        public void Book_RepositoryTests_UpdateBook_InvalidId_ShouldBeFail()
        {
            Book bookToUpdate = ObjectMother.GetBookOK();
            bookToUpdate.Id = 0;

            Action comparation = () => _bookRepository.Update(bookToUpdate);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Book_RepositoryTests_GetBook_ShouldBeOk()
        {
            Book bookToFind = ObjectMother.GetBookOK();

            Book bookFound = _bookRepository.Get(bookToFind.Id);

            bookFound.Should().NotBeNull();
            bookFound.Id.Should().Be(bookToFind.Id);
        }

        [Test]
        public void Book_RepositoryTests_GetBook_InvalidId_ShouldBeFail()
        {
            Book bookToFind = ObjectMother.GetBookOK();
            bookToFind.Id = 0;

            Action comparation = () => _bookRepository.Get(bookToFind.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Book_RepositoryTests_GetBook_ShouldBeNull()
        {
            Book bookToFind = ObjectMother.GetBookOK();
            bookToFind.Id = 5;

            Book bookFound = _bookRepository.Get(bookToFind.Id);

            bookFound.Should().BeNull();
        }

        [Test]
        public void Book_RepositoryTests_GetAll_ShouldBeOk()
        {
            int expectedAmount = 1;
            int id = 1;
            var listBooks = _bookRepository.GetAll();

            listBooks.Should().NotBeNullOrEmpty();
            listBooks.Count().Should().Be(expectedAmount);
            listBooks.First().Id.Should().Be(id);
        }

        [Test]
        public void Book_RepositoryTests_GetAll_ShouldBeNull()
        {
            BaseSqlTests.ClearDataBase();

            var  listBooks = _bookRepository.GetAll();

            listBooks.Should().BeNullOrEmpty();
        }

        [Test]
        public void Book_RepositoryTests_DeleteBook_InvalidId_ShouldBeFail()
        {
            Book bookToDelete = ObjectMother.GetBookOK();
            bookToDelete.Id = 0;

            Action comparation = () => _bookRepository.Delete(bookToDelete);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Book_RepositoryTests_DeleteBook_ShouldBeOk()
        {
            BaseSqlTests.ClearTBLoan();
            Book bookToDelete = ObjectMother.GetBookOK();

            _bookRepository.Delete(bookToDelete);

            var listBooks = _bookRepository.GetAll();
            listBooks.Should().BeNullOrEmpty();
        }
    }
}
