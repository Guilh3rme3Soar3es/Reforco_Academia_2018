using Biblioteca.Common.Tests.Features.ObjectMothers;
using Biblioteca.Domain.Features.Books;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Tests.Features.Books
{
    [TestFixture]
    public class BookDomainTests
    {
        private Book _book;

        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Book_TestDomain_Book_ShouldBeOk()
        {
            _book = ObjectMother.GetBookOK();

            Action comparation = () => _book.Validate();

            comparation.Should().NotThrow();
        }

        [Test]
        public void Book_TestDomain_BookTitleNull_ShouldBeFail()
        {
            _book.Title = null;

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookThemeNullOremptyException>();
        }

        [Test]
        public void Book_TestDomain_BookTitleEmpty_ShouldBeFail()
        {
            _book.Title = "";

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookThemeNullOremptyException>();
        }

        [Test]
        public void Book_TestDomain_BookWithShortTitle_ShouldBeFail()
        {
            _book.Title = "ABC";

            Action comparation = () => _book.Validate();

            comparation.Should().NotThrow<BookShortTitleException>();
        }

        [Test]
        public void Book_TestDomain_BookTitleOverFlow_ShouldBeFail()
        {
            _book.Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _book.Validate();

            comparation.Should().NotThrow<BookTitleOverFlowException>();
        }


        [Test]
        public void Book_TestDomain_BookThemeNull_ShouldBeFail()
        {
            _book.Theme = null;

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookThemeNullOremptyException>();
        }

        [Test]
        public void Book_TestDomain_BookThemeEmpty_ShouldBeFail()
        {
            _book.Theme = "";

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookThemeNullOremptyException>();
        }

        [Test]
        public void Book_TestDomain_BookWithShortTheme_ShouldBeFail()
        {
            _book.Theme = "ABC";

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookShortThemeException>();
        }

        [Test]
        public void Book_TestDomain_BookThemeOverFlow_ShouldBeFail()
        {
            _book.Theme = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookThemeOverFlowException>();
        }

        [Test]
        public void Book_TestDomain_BookInvalidVolume_ShouldBeFail()
        {
            _book.Volume = 0;

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookInvalidVolumeException>();
        }

        [Test]
        public void Book_TestDomain_BookInvalidPostDate_ShouldBeFail()
        {
            _book.PostDate = DateTime.Now.AddDays(+1);

            Action comparation = () => _book.Validate();

            comparation.Should().Throw<BookInvalidPostDateException>();
        }

        

        
        
    }
}
