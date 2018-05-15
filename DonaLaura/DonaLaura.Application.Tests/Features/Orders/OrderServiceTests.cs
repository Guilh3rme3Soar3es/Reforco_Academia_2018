using DonaLaura.Application.Features.Orders;
using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Exceptions;
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

namespace DonaLaura.Application.Tests.Features.Orders
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Product _product;
        private Order _order;
        private Mock<IOrderRepository> _mockRepository;
        private OrderService _service;

        [SetUp]
        public void Initialize()
        {
            _product = ObjectMother.GetProductOk();
            _order = ObjectMother.GetOrderOk(_product);
            _mockRepository = new Mock<IOrderRepository>();
            _service = new OrderService(_mockRepository.Object);
        }

        [Test]
        public void Order_TestService_AddOrder_ShouldBeOk()
        {
            _mockRepository.Setup(or => or.Save(_order)).Returns(_order);

            Order resultadoEncontrado = _service.Add(_order);

            _mockRepository.Verify(or => or.Save(_order));
            resultadoEncontrado.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Order_TestService_AddOrder_ShouldBeFail()
        {
            _order.Client = "";
            _mockRepository.Setup(or => or.Save(_order));

            Action comparation = () => _service.Add(_order);

            comparation.Should().Throw<OrderClientNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Order_TestService_UpdateOrder_ShouldBeOk()
        {
            _order.Client = "Teste de Atualização";
            _mockRepository.Setup(or => or.Update(_order)).Returns(_order);

            Order resultadoEncontrado = _service.Update(_order);

            _mockRepository.Verify(or => or.Update(_order));
            resultadoEncontrado.Id.Should().BeGreaterThan(0);
            resultadoEncontrado.Client.Should().Be("Teste de Atualização");
        }

        [Test]
        public void Order_TestService_UpdateOrder_ShouldBeFail()
        {
            _order.Client = "";
            _mockRepository.Setup(or => or.Update(_order));

            Action comparation = () => _service.Update(_order);

            comparation.Should().Throw<OrderClientNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Order_TestService_GetOrder_ShouldBeOk()
        {
            _mockRepository.Setup(or => or.Get(_order.Id)).Returns(_order);

            Order resultadoEncontrado = _service.Get(_order.Id);

            _mockRepository.Verify(pr => pr.Get(_order.Id));
            resultadoEncontrado.Id.Should().BeGreaterThan(0);
            resultadoEncontrado.Id.Should().Be(_order.Id);
        }

        [Test]
        public void Order_TestService_GetOrder_ShouldBeNull()
        {
            _mockRepository.Setup(pr => pr.Get(_order.Id));

            var listPosts = _service.Get(_order.Id);

            listPosts.Should().BeNull();
            _mockRepository.Verify(osv => osv.Get(_order.Id));
        }

        [Test]
        public void Order_TestService_GetOrder_ShouldBeFail()
        {
            _order.Id = -1;
            _mockRepository.Setup(or => or.Get(_order.Id));

            Action comparation = () => _service.Get(_order.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Order_TestService_GetAllOrder_ShouldBeOk()
        {
            _mockRepository.Setup(or => or.GetAll()).Returns(new List<Order> { _order });

            IEnumerable<Order> resultadoEncontrado = _service.GetAll();

            resultadoEncontrado.Should().BeEquivalentTo(new List<Order> { _order });
            _mockRepository.Verify(or => or.GetAll());
        }

        [Test]
        public void Order_TestService_GetAllOrder_ShouldBeNull()
        {
            _mockRepository.Setup(or => or.GetAll());

            var listPosts = _service.GetAll();

            listPosts.Should().BeNull();
            _mockRepository.Verify(psv => psv.GetAll());
        }

        [Test]
        public void Order_TestService_DeleteOrder_ShouldBeOk()
        {
            _mockRepository.Setup(or => or.Delete(_order));
            _service.Delete(_order);
            _mockRepository.Verify(or => or.Delete(_order));
        }

        [Test]
        public void Order_TestService_DeleteOrder_ShouldBeFail()
        {
            _order.Id = -1;
            _mockRepository.Setup(or => or.Delete(_order));

            Action comparation = () => _service.Delete(_order);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Order_TestService_GetByProduct_ShouldBeOk()
        {
            _mockRepository.Setup(or => or.GetByProduct(_order.product.Id)).Returns(new List<Order> { _order });

            IEnumerable<Order> resultadoEncontrado = _service.GetByProduct(_order.product.Id);

            resultadoEncontrado.Should().BeEquivalentTo(new List<Order> { _order });
            _mockRepository.Verify(pr => pr.GetByProduct(_order.product.Id));
        }

        [Test]
        public void Order_TestService_GetByProduct_ShouldBeNull()
        {
            _mockRepository.Setup(or => or.GetByProduct(_order.product.Id));

            IEnumerable<Order> resultadoEncontrado = _service.GetByProduct(_order.product.Id);

            resultadoEncontrado.Should().BeNull();
            _mockRepository.Verify(pr => pr.GetByProduct(_order.product.Id));
        }

        [Test]
        public void Order_TestService_GetByProduct_InvalidId_ShouldBeFail()
        {
            _order.product.Id = -1;
            _mockRepository.Setup(or => or.GetByProduct(_order.product.Id));

            Action comparation = () => _service.GetByProduct(_order.product.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
