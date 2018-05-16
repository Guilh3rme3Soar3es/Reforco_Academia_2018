using DonaLaura.Application.Features.Orders;
using DonaLaura.Application.Features.Products;
using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Orders;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Common.Tests.Features.ObjectMother;
using DonaLaura.Infra.Data.Features.Orders;
using DonaLaura.Infra.Data.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Integration.Tests.Features.Orders
{
    [TestFixture]
    public class OrderIntegrationSqlTests
    {
        private OrderService _orderService;
        private OrderRepository _orderRepositopty;
        private Product _product;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();

            _product = ObjectMother.GetProductOk();

            _orderRepositopty = new OrderRepository();
            _orderService = new OrderService(_orderRepositopty);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_ShouldBeOk()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);

            Order orderSaved = _orderService.Add(orderToSave);

            Order resultadoEncontrado = _orderService.Get(orderSaved.Id);
            orderSaved.Should().NotBeNull();
            resultadoEncontrado.Id.Should().Be(orderSaved.Id);
            orderSaved.Client.Should().Be(orderToSave.Client);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_ClientNameNullOrEmpty_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Client = null;

            Action comparation = () => _orderService.Add(orderToSave);

            comparation.Should().Throw<OrderClientNullOrEmptyException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_ClientNameShort_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Client = "ABC";

            Action comparation = () => _orderService.Add(orderToSave);

            comparation.Should().Throw<OrderClientNameShortException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_ClientNameOverFlow_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Client = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _orderService.Add(orderToSave);

            comparation.Should().Throw<OrderClientNameOverFlowException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_AmountZero_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Amount = 0;

            Action comparation = () => _orderService.Add(orderToSave);

            comparation.Should().Throw<OrderAmountZeroException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_ProductNull_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.product = null;

            Action comparation = () => _orderService.Add(orderToSave);

            comparation.Should().Throw<OrderProductNullException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_SaveOrder_ProductNotAvaliable_ShouldBeFail()
        {
            _product.Id = 1;
            _product.IsAvaliable = false;
            Order orderToSave = ObjectMother.GetOrderOk(_product);

            Action comparation = () => _orderService.Add(orderToSave);

            comparation.Should().Throw<OrderWithProductNotAvaliableException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_ShouldBeOk()
        {
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Id = 1;
            orderToUpdate.Client = "Teste de Atualização";

            Order orderSaved = _orderService.Update(orderToUpdate);

            Order resultadoEncontrado = _orderService.Get(orderSaved.Id);
            orderSaved.Should().NotBeNull();
            resultadoEncontrado.Id.Should().Be(orderSaved.Id);
            orderSaved.Client.Should().Be(orderToUpdate.Client);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_ClientNameNullOrEmpty_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Client = null;

            Action comparation = () => _orderService.Update(orderToUpdate);

            comparation.Should().Throw<OrderClientNullOrEmptyException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_ClientNameShort_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Client = "ABC";

            Action comparation = () => _orderService.Update(orderToUpdate);

            comparation.Should().Throw<OrderClientNameShortException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_ClientNameOverFlow_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Client = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _orderService.Update(orderToUpdate);

            comparation.Should().Throw<OrderClientNameOverFlowException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_AmountZero_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Amount = 0;

            Action comparation = () => _orderService.Update(orderToUpdate);

            comparation.Should().Throw<OrderAmountZeroException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_ProductNull_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.product = null;

            Action comparation = () => _orderService.Update(orderToUpdate);

            comparation.Should().Throw<OrderProductNullException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_UpdateOrder_ProductNotAvaliable_ShouldBeFail()
        {
            _product.Id = 1;
            _product.IsAvaliable = false;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);

            Action comparation = () => _orderService.Update(orderToUpdate);

            comparation.Should().Throw<OrderWithProductNotAvaliableException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_DeleteOrder__ShouldBeOk()
        {
            _product.Id = 1;
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = 1;

            _orderService.Delete(orderToDelete);

            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(0);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_DeleteOrder_InvalidId_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = -1;

            Action comparation = () => _orderService.Delete(orderToDelete);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_DeleteOrder_OrderNotFound_ShouldBeFail()
        {
            _product.Id = 1;
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = 2;

            _orderService.Delete(orderToDelete);

            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
            productRetorns.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetAll__ShouldBeOk()
        {
            int id = 1;

            var productReturns = _orderService.GetAll();

            productReturns.Count().Should().Be(1);
            productReturns.First().Id.Should().Be(id);
            productReturns.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetAll__ShouldBeNull()
        {
            _product.Id = 1;
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = 1;

            _orderService.Delete(orderToDelete);

            var productReturns = _orderService.GetAll();

            productReturns.Count().Should().Be(0);
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetOrder_ShouldBeOk()
        {
            int id = 1;

            Order orderFinded = _orderService.Get(id);

            orderFinded.Should().NotBeNull();
            orderFinded.Id.Should().Be(1);
            orderFinded.Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetOrder_InvalidId_ShouldBeFail()
        {
            var id = -1;

            Action comparation = () => _orderService.Get(id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetOrder_ShouldBeFail()
        {
            var id = 2;
            Order orderReturns = _orderService.Get(id);

            orderReturns.Should().BeNull();
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetByProduct_ShouldBeOk()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Id = 1;

            var listorders = _orderService.GetByProduct(orderToSave.product.Id);

            listorders.Count().Should().Be(1);
            listorders.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestSystemIntegrationSql_GetByProduct_InvalidId_ShouldBeFail()
        {
            _product.Id = -1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Id = -1;

            Action comparation = () => _orderService.GetByProduct(orderToSave.product.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var productRetorns = _orderService.GetAll();
            productRetorns.Count().Should().Be(1);
        }
    }
}
