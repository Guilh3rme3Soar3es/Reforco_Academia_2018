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

namespace Biblioteca.Domain.Tests.Features.Loans
{
    [TestFixture]
    public class LoanDomainTests
    {
        private Loan _loan;
        private Mock<Book> _fakeBook;
        [SetUp]
        public void Initialize()
        {
            _fakeBook = new Mock<Book>();
        }

        [Test]
        public void Loan_DomainTests_Loan_ShouldBeOk()
        {
            _fakeBook.Setup(b => b.IsAvaliable).Returns(true);
            _loan = ObjectMother.GetLoanOk(_fakeBook.Object);

            Action comparation = () => _loan.Validate();

            comparation.Should().NotThrow();
        }

        [Test]
        public void Loan_DomainTests_LoanClientNameNull_ShouldBeFail()
        {
            _fakeBook.Setup(b => b.IsAvaliable).Returns(true);
            _loan = ObjectMother.GetLoanOk(_fakeBook.Object);
            _loan.ClientName = null;

            Action comparation = () => _loan.Validate();

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void Loan_DomainTests_LoanClientNameEmpty_ShouldBeFail()
        {
            _fakeBook.Setup(b => b.IsAvaliable).Returns(true);
            _loan = ObjectMother.GetLoanOk(_fakeBook.Object);
            _loan.ClientName = "";

            Action comparation = () => _loan.Validate();

            comparation.Should().Throw<LoanClientNameNullOrEmptyException>();
        }

        [Test]
        public void Loan_DomainTests_LoanClientNameOverFlow_ShouldBeFail()
        {
            _fakeBook.Setup(b => b.IsAvaliable).Returns(true);
            _loan = ObjectMother.GetLoanOk(_fakeBook.Object);
            _loan.ClientName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _loan.Validate();

            comparation.Should().Throw<LoanClientNameOverFlowException>();
        }

        [Test]
        public void Loan_DomainTests_LoanWithBookNull_ShouldBeFail()
        {
            _fakeBook.Setup(b => b.IsAvaliable).Returns(true);
            _loan = ObjectMother.GetLoanOk(_fakeBook.Object);
            _loan.Book = null;

            Action comparation = () => _loan.Validate();

            comparation.Should().Throw<LoanNoBookException>();
        }

        [Test]
        public void Loan_DomainTests_LoanWithBookNotAvaliable_ShouldBeFail()
        {
            _fakeBook.Setup(b => b.IsAvaliable).Returns(false);
            _loan = ObjectMother.GetLoanOk(_fakeBook.Object);

            Action comparation = () => _loan.Validate();

            comparation.Should().Throw<LoanWithBookNotAvaliableException>();
        }


    }
}
