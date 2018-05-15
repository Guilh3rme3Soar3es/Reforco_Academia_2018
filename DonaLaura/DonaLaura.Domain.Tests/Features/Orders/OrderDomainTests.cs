using DonaLaura.Domain.Features.Orders;
using DonaLaura.Domain.Features.Products;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Tests.Features.Orders
{
    [TestFixture]
    public class OrderDomainTests
    {
        private Mock<Product> _fakeProduct;
        private Order _order;
        [SetUp]
        public void Initialize()
        {
            _fakeProduct = new Mock<Product>();
            _fakeProduct.Setup(m => m.Id).Returns(2);
            _fakeProduct.Setup(m => m.Name).Returns("Perfume Show");
            _fakeProduct.Setup(m => m.SalePrice).Returns(200.00);
            _fakeProduct.Setup(m => m.CostPrice).Returns(150.00);
            _fakeProduct.Setup(m => m.Manufacture).Returns(DateTime.Now.AddDays(-1));
            _fakeProduct.Setup(m => m.Expiration).Returns(DateTime.Now.AddDays(+1));
            _fakeProduct.Setup(m => m.IsAvaliable).Returns(true);

            _order = ObjectMother.GetOrder(_fakeProduct.Object);
        }

        [Test]
        public void Order_TestDomain_Order_ShouldBeOk()
        {
            Action comparation = () => _order.Validate();

            comparation.Should().NotThrow();
        }

        [Test]
        public void Order_TestDomain_OrderNameClient_ShouldBeOk()
        {
            _order.Client = "João da Silva";

            Action comparation = () => _order.Validate();

            comparation.Should().NotThrow<OrderClientNullOrEmptyException>();
        }

        [Test]
        public void Order_TestDomain_OrderNameClientNullOrEmpty_ShouldBeFail()
        {
            _order.Client = "";

            Action comparation = () => _order.Validate();

            comparation.Should().Throw<OrderClientNullOrEmptyException>();
        }

        [Test]
        public void Order_TestDomain_OrderClientNameShort_ShouldBeFail()
        {
            _order.Client = "ABC";

            Action comparation = () => _order.Validate();

            comparation.Should().Throw<OrderClientNameShortException>();
        }

        [Test]
        public void Order_TestDomain_OrderProductNull_ShouldBeFail()
        {
            _order.product = null;

            Action comparation = () => _order.Validate();

            comparation.Should().Throw<OrderProductNullException>();
        }

        [Test]
        public void Order_TestDomain_OrderAmountZero_ShouldBeFail()
        {
            _order.Amount = 0;

            Action comparation = () => _order.Validate();

            comparation.Should().Throw<OrderAmountZeroException>();
        }


        //verificar necessidade...
        //[Test]
        public void TesteDeProduct()
        {
            _fakeProduct.Setup(m => m.Name);
            _fakeProduct.Setup(m => m.Validate());
            _order.product = _fakeProduct.Object;

            Action comparation = () => _order.Validate();

            _fakeProduct.Verify(m => m.Validate());
            comparation.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void Order_TestDomain_OrderGetProfit_ShouldBeOk()
        {
            _fakeProduct.Setup(m => m.SalePrice).Returns(300.00);
            _fakeProduct.Setup(m => m.CostPrice).Returns(150.00);
            _order.product = _fakeProduct.Object;

            _order.GetProfit();

            _order.Profit.Should().Be(150.00);
        }
    }
}
