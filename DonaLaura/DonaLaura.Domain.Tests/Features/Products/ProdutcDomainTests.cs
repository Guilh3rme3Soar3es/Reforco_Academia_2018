using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Tests.Features.Products
{
    [TestFixture]
    public class ProdutcDomainTests
    {
        Product _product;
        [SetUp]
        public void Initialize()
        {
            _product = ObjectMother.GetProductOk();
        }

        [Test]
        public void Product_TestDomain_Product_ShouldBeOk()
        {
            Action comparation = () => _product.Validate();

            comparation.Should().NotThrow();
        }

        [Test]
        public void Product_TestDomain_ProductName_ShouldBeOk()
        {
            _product.Name = "Perfume francês";

            Action comparation = () => _product.Validate();

            comparation.Should().NotThrow<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void Product_TestDomain_ProductNameEmpty_ShouldBeFail()
        {
            _product.Name = "";

            Action comparation = () => _product.Validate();

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void Product_TestDomain_ProductNameNull_ShouldBeFail()
        {
            _product.Name = null;

            Action comparation = () => _product.Validate();

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void Product_TestDomain_ProductNameShort_ShouldBeFail()
        {
            _product.Name = "ABCD";

            Action comparation = () => _product.Validate();

            comparation.Should().Throw<ProductNameShortException>();
        }

        [Test]
        public void Product_TestDomain_ProductNameOverFlow_ShouldBeFail()
        {
            _product.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _product.Validate();

            comparation.Should().Throw<ProductNameOverFlowException>();
        }

        [Test]
        public void Product_TestDomain_ProductCostPrice_ShouldBeOk()
        {
            _product.SalePrice = 6.00;
            _product.CostPrice = 5.00;

            Action comparation = () => _product.Validate();

            comparation.Should().NotThrow<ProductCostPriceOverFlow>();
        }

        [Test]
        public void Product_TestDomain_ProductCostPrice_ShouldBeFail()
        {
            _product.SalePrice = 5.00;
            _product.CostPrice = 6.00;

            Action comparation = () => _product.Validate();

            comparation.Should().Throw<ProductCostPriceOverFlow>();
        }

        [Test]
        public void Product_TestDomain_ProductExpiration_ShouldBeOk()
        {
            _product.Manufacture = DateTime.Now.AddDays(-1);
            _product.Expiration = DateTime.Now.AddDays(+1);

            Action comparation = () => _product.Validate();

            comparation.Should().NotThrow<ProductExpirationInvalidException>();
        }

        [Test]
        public void Product_TestDomain_ProductExpiration_ShouldBeFail()
        {
            _product.Manufacture = DateTime.Now.AddDays(+1);
            _product.Expiration = DateTime.Now.AddDays(-1);

            Action comparation = () => _product.Validate();

            comparation.Should().Throw<ProductExpirationInvalidException>();
        }
    }
}
