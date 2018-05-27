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

namespace Biblioteca.Application.Tests.Features.Loans
{
    [TestFixture]
    public class LoanApplicationTests
    {
        private Mock<ILoanRepository> _mockLoanRepository;
        private LoanService _loanService;

        private Book _book;
        private Loan _loan;
        [SetUp]
        public void Initialize()
        {
            _mockLoanRepository = new Mock<ILoanRepository>();
            _loanService = new LoanService(_mockLoanRepository.Object);

            _book = ObjectMother.GetBookOK();
            _loan = ObjectMother.GetLoanOk(_book);
        }

        [Test]
        public void Loan_TesteService_AddLoan_SouldBeOk()
        {
            _mockLoanRepository.Setup(lr => lr.Save(_loan)).Returns(_loan);

            Loan loanSaved = _loanService.Add(_loan);

            loanSaved.Id.Should().Be(_book.Id);
            _mockLoanRepository.Verify(lr => lr.Save(_loan));
        }

        [Test]
        public void Loan_TesteService_AddLoan_InvalidLoan_SouldBeFail()
        {
            _loan.ClientName = "";
            _mockLoanRepository.Setup(lr => lr.Save(_loan)).Returns(_loan);

            Action comparation = () => _loanService.Add(_loan);

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
            _mockLoanRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Loan_TesteService_UpdateLoan_SouldBeOk()
        {
            _mockLoanRepository.Setup(lr => lr.Update(_loan)).Returns(_loan);

            Loan loanUpdated = _loanService.Update(_loan);

            loanUpdated.Id.Should().Be(_book.Id);
            _mockLoanRepository.Verify(lr => lr.Update(_loan));
        }

        [Test]
        public void Loan_TesteService_UpdateLoan_InvalidId_SouldBeFail()
        {
            _loan.Id = -1;
            _mockLoanRepository.Setup(lr => lr.Update(_loan)).Returns(_loan);

            Action comparation = () => _loanService.Update(_loan);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockLoanRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Loan_TesteService_UpdateLoan_InvalidLoan_SouldBeFail()
        {
            _loan.ClientName = "";
            _mockLoanRepository.Setup(lr => lr.Update(_loan)).Returns(_loan);

            Action comparation = () => _loanService.Update(_loan);

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
            _mockLoanRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Loan_TesteService_GetLoan_SouldBeOk()
        {
            _mockLoanRepository.Setup(lr => lr.Get(_loan.Id)).Returns(_loan);

            Loan bookFound = _loanService.Get(_loan.Id);

            bookFound.Should().NotBeNull();
            _mockLoanRepository.Verify(lr => lr.Get(_loan.Id));
        }

        [Test]
        public void Loan_TesteService_GetLoan_InvalidId_SouldBeFail()
        {
            _loan.Id = 0;
            _mockLoanRepository.Setup(lr => lr.Get(_loan.Id)).Returns(_loan);

            Action comparation = () => _loanService.Get(_loan.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockLoanRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Loan_TesteService_GetAll_SouldBeOk()
        {
            _mockLoanRepository.Setup(lr => lr.GetAll()).Returns(new List<Loan> { _loan });

            var lisitLoans = _loanService.GetAll();

            lisitLoans.Should().NotBeNullOrEmpty();
            _mockLoanRepository.Verify(lr => lr.GetAll());
        }

        [Test]
        public void Loan_TesteService_DeleteLoan_SouldBeOk()
        {
            _mockLoanRepository.Setup(lr => lr.Delete(_loan));

            _loanService.Delete(_loan);

            _mockLoanRepository.Verify(lr => lr.Delete(_loan));
        }

        [Test]
        public void Loan_TesteService_DeleteBook_InvalidId_SouldBeFail()
        {
            _loan.Id = 0;
            _mockLoanRepository.Setup(br => br.Delete(_loan));

            Action comparation = () => _loanService.Delete(_loan);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockLoanRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Loan_TesteService_GetByBook_InvalidId_SouldBeFail()
        {
            _loan.Book.Id = 0;
            _mockLoanRepository.Setup(br => br.GetByBook(_loan.Book.Id));

            Action comparation = () => _loanService.GetByBook(_loan.Book.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockLoanRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Loan_TesteService_GetByBook_SouldBeOk()
        {
            _mockLoanRepository.Setup(br => br.GetByBook(_loan.Book.Id)).Returns(new List<Loan> { _loan });

            var  listLoans = _loanService.GetByBook(_loan.Book.Id);

            listLoans.Should().NotBeNullOrEmpty();
            _mockLoanRepository.Verify(lr => lr.GetByBook(_loan.Book.Id));
        }

    }
}
