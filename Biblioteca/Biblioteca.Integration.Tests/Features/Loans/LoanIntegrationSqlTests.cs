using Biblioteca.Application.Features.Loans;
using Biblioteca.Common.Tests.Base;
using Biblioteca.Common.Tests.Features.ObjectMothers;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Books;
using Biblioteca.Domain.Features.Loans;
using Biblioteca.Infra.Data.Features.Loans;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Integration.Tests.Features.Loans
{
    [TestFixture]
    public class LoanIntegrationSqlTests
    {
        private LoanRepository _loanRepository;
        private LoanService _loanService;

        private Book _book;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _loanRepository = new LoanRepository();
            _loanService = new LoanService(_loanRepository);

            _book = ObjectMother.GetBookOK();
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_AddLoan_ShouldBeOk()
        {
            int id = 1;
            int expectedAmount = 2;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);

            Loan loanSaved = _loanService.Add(loanToSave);

            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_AddLoan_NameClientNullOrEmpty_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);
            loanToSave.ClientName = "";

            Action comparation = () => _loanService.Add(loanToSave);

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_AddLoan_NameClientOverFlow_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);
            loanToSave.ClientName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _loanService.Add(loanToSave);

            comparation.Should().Throw<LoanClientNameOverFlowException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_AddLoan_InvalidDateDevolition_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);
            loanToSave.DateDevolution = DateTime.Now.AddDays(-1);

            Action comparation = () => _loanService.Add(loanToSave);

            comparation.Should().Throw<LoanInvalidDateDevolutionException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }


        [Test]
        public void Loan_TestSystemIntegrationSql_AddLoan_BookNull_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);
            loanToSave.Book = null;

            Action comparation = () => _loanService.Add(loanToSave);

            comparation.Should().Throw<LoanNoBookException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_AddLoan_BookNotAvalible_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);
            loanToSave.Book.IsAvaliable = false;

            Action comparation = () => _loanService.Add(loanToSave);

            comparation.Should().Throw<LoanWithBookNotAvaliableException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_UpdateLoan_ShouldBeOk()
        {
            int id = 2;
            Loan loanToUpdate = ObjectMother.GetLoanOk(_book);
            loanToUpdate.ClientName = "Teste de atualização";

            Loan loanUpdated = _loanService.Add(loanToUpdate);

            loanUpdated.Should().NotBeNull();
            loanUpdated.ClientName.Should().Be(loanToUpdate.ClientName);
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Last().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_UpdateLoan_NameClientNullOrEmpty_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToSave = ObjectMother.GetLoanOk(_book);
            loanToSave.ClientName = "";

            Action comparation = () => _loanService.Update(loanToSave);

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_UpdateLoan_NameClientOverFlow_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToUpdate = ObjectMother.GetLoanOk(_book);
            loanToUpdate.ClientName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _loanService.Update(loanToUpdate);

            comparation.Should().Throw<LoanClientNameOverFlowException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_UpdateLoan_InvalidDateDevolition_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToUpdate = ObjectMother.GetLoanOk(_book);
            loanToUpdate.DateDevolution = DateTime.Now.AddDays(-1);

            Action comparation = () => _loanService.Update(loanToUpdate);

            comparation.Should().Throw<LoanInvalidDateDevolutionException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }


        [Test]
        public void Loan_TestSystemIntegrationSql_UpdateLoan_BookNull_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToUpdate = ObjectMother.GetLoanOk(_book);
            loanToUpdate.Book = null;

            Action comparation = () => _loanService.Update(loanToUpdate);

            comparation.Should().Throw<LoanNoBookException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_UpdateLoan_BookNotAvalible_ShouldBeFail()
        {
            int id = 1;
            int expectedAmount = 1;
            Loan loanToUpdate = ObjectMother.GetLoanOk(_book);
            loanToUpdate.Book.IsAvaliable = false;

            Action comparation = () => _loanService.Update(loanToUpdate);

            comparation.Should().Throw<LoanWithBookNotAvaliableException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetLoan_ShouldBeOk()
        {
            Loan loanToFind = ObjectMother.GetLoanOk(_book);

            Loan loanFound = _loanService.Get(loanToFind.Id);

            loanFound.Should().NotBeNull();
            loanFound.Id.Should().Be(loanToFind.Id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetLoan_InvalidId_ShouldBeFail()
        {
            Loan loanToFind = ObjectMother.GetLoanOk(_book);
            loanToFind.Id = 0;

            Action comparation = () => _loanService.Get(loanToFind.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetLoan_ShouldBeNull()
        {
            Loan loanToFind = ObjectMother.GetLoanOk(_book);
            loanToFind.Id = 5;

            Loan loanFound = _loanService.Get(loanToFind.Id);

            loanFound.Should().BeNull();
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetAll_ShouldBeOk()
        {
            int id = 1;
            int expectedAmount = 1;
            var listLoans = _loanService.GetAll();

            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetAll_ShouldBeNull()
        {
            BaseSqlTests.ClearDataBase();

            var  listLoans = _loanService.GetAll();

            listLoans.Should().BeNullOrEmpty();
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_DeleteLoan_ShouldBeOk()
        {
            Loan loanToDelete = ObjectMother.GetLoanOk(_book);

            _loanService.Delete(loanToDelete);

            var listLoans = _loanService.GetAll();
            listLoans.Should().BeNullOrEmpty();
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_DeleteLoan_InvalidId_ShouldBeFail()
        {
            int expectedAmount = 1;
            Loan loanToDelete = ObjectMother.GetLoanOk(_book);
            loanToDelete.Id = 0;

            Action comparation = () => _loanService.Delete(loanToDelete);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var listLoans = _loanService.GetAll();
            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetByBook_ShouldBeOk()
        {
            int expectedAmount = 1;
            Loan loanToFind = ObjectMother.GetLoanOk(_book);
            loanToFind.Book.Id = 1;

            var listLoans = _loanService.GetByBook(loanToFind.Book.Id);

            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetByBook_InvalidId_ShouldBeFail()
        {
            Loan loanToFind = ObjectMother.GetLoanOk(_book);
            loanToFind.Book.Id = 0;

            Action comparation = () => _loanService.GetByBook(loanToFind.Book.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Loan_TestSystemIntegrationSql_GetByBook_ShouldBeNull()
        {
            Loan loanToFind = ObjectMother.GetLoanOk(_book);
            loanToFind.Book.Id = 5;

            var listLoans = _loanService.GetByBook(loanToFind.Book.Id);

            listLoans.Should().BeNullOrEmpty();
        }
    }
}
