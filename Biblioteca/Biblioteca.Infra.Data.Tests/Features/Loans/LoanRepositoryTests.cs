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

namespace Biblioteca.Infra.Data.Tests.Features.Loans
{
    [TestFixture]
    public class LoanRepositoryTests
    {
        private LoanRepository _loanRepository;

        private Book _book;
        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _loanRepository = new LoanRepository();

            _book = ObjectMother.GetBookOK();
        }

        [Test]
        public void Loan_RepositoryTests_AddLoan_ShouldBeOk()
        {
            int id = 2;
            Loan LoanToSave = ObjectMother.GetLoanOk(_book);

            Loan loanSaved = _loanRepository.Save(LoanToSave);

            loanSaved.Id.Should().Be(id);
            loanSaved.ClientName.Should().Be(LoanToSave.ClientName);
        }

        [Test]
        public void Loan_RepositoryTests_AddLoan_InvalidLoan_ShouldBeFail()
        {
            Loan LoanToSave = ObjectMother.GetLoanOk(_book);
            LoanToSave.ClientName = "";

            Action comparation = () => _loanRepository.Save(LoanToSave);

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void Loan_RepositoryTests_UpdateLoan_ShouldBeOk()
        {
            int id = 2;
            Loan LoanToUpdate = ObjectMother.GetLoanOk(_book);
            LoanToUpdate.ClientName = "Teste de Atualização";

            Loan loanUpdated = _loanRepository.Update(LoanToUpdate);

            loanUpdated.Should().NotBeNull();
            loanUpdated.ClientName.Should().Be(LoanToUpdate.ClientName);
        }

        [Test]
        public void Loan_RepositoryTests_UpdateLoan_InvalidLoan_ShouldBeFail()
        {
            Loan LoanToUpdate = ObjectMother.GetLoanOk(_book);
            LoanToUpdate.ClientName = "";

            Action comparation = () => _loanRepository.Update(LoanToUpdate);

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void Loan_RepositoryTests_UpdateLoan_InvalidId_ShouldBeFail()
        {
            Loan LoanToUpdate = ObjectMother.GetLoanOk(_book);
            LoanToUpdate.Id = 0;

            Action comparation = () => _loanRepository.Update(LoanToUpdate);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Loan_RepositoryTests_GetLoan_ShouldBeOk()
        {
            Loan LoanToFind = ObjectMother.GetLoanOk(_book);

            Loan loanFound = _loanRepository.Get(LoanToFind.Id);

            loanFound.Should().NotBeNull();
            loanFound.Id.Should().Be(LoanToFind.Id);
            loanFound.Book.Should().NotBeNull();
        }

        [Test]
        public void Loan_RepositoryTests_GetLoan_InvalidId_ShouldBeFail()
        {
            Loan LoanToFind = ObjectMother.GetLoanOk(_book);
            LoanToFind.Id = 0;

            Action comparation = () => _loanRepository.Get(LoanToFind.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Loan_RepositoryTests_GetLoan_ShouldBeNull()
        {
            Loan LoanToFind = ObjectMother.GetLoanOk(_book);
            LoanToFind.Id = 5;

            Loan loanFound = _loanRepository.Get(LoanToFind.Id);

            loanFound.Should().BeNull();
        }

        [Test]
        public void Loan_RepositoryTests_GetAll_ShouldBeOk()
        {
            int expectedAmount = 1;
            int id = 1;
            var listLoans = _loanRepository.GetAll();

            listLoans.Should().NotBeNullOrEmpty();
            listLoans.Count().Should().Be(expectedAmount);
            listLoans.First().Id.Should().Be(id);
            listLoans.First().Book.Should().NotBeNull();
        }

        [Test]
        public void Loan_RepositoryTests_GetAll_ShouldBeNull()
        {
            BaseSqlTests.ClearDataBase();

            var listLoans = _loanRepository.GetAll();

            listLoans.Should().BeNullOrEmpty();
        }

        [Test]
        public void Loan_TestRepository_DeleteLoan_ShouldBeOk()
        {
            Loan LoanToDelete = ObjectMother.GetLoanOk(_book);

            _loanRepository.Delete(LoanToDelete);

            var listLoans = _loanRepository.GetAll();

            listLoans.Should().BeNullOrEmpty();
        }

        [Test]
        public void Loan_RepositoryTests_DeleteLoan_InvalidId_ShouldBeFail()
        {
            Loan LoanToDelete = ObjectMother.GetLoanOk(_book);
            LoanToDelete.Id = 0;

            Action comparation = () => _loanRepository.Delete(LoanToDelete);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Loan_RepositoryTests_GetLoanByBook_ShouldBeOk()
        {
            Loan LoanToFind = ObjectMother.GetLoanOk(_book);

            var listLoans = _loanRepository.GetByBook(LoanToFind.Book.Id);

            listLoans.Should().NotBeNull();
            listLoans.First().Id.Should().Be(LoanToFind.Id);
            listLoans.First().Book.Should().NotBeNull();
        }

        [Test]
        public void Loan_RepositoryTests_GetLoanByBook_InvalidId_ShouldBeFail()
        {
            Loan LoanToDelete = ObjectMother.GetLoanOk(_book);
            LoanToDelete.Book.Id = 0;

            Action comparation = () => _loanRepository.GetByBook(LoanToDelete.Book.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
