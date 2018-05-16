using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Orders;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Common.Tests.Features.ObjectMother;
using DonaLaura.Infra.Data.Features.Orders;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Tests.Features.Orders
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private OrderRepository _repository;
        private Product _product;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();

            _repository = new OrderRepository();

            _product = ObjectMother.GetProductOk();
        }

        [Test]
        public void Order_TestRepository_SaveOrder_ShouldBeOk()
        {
            _product.Id = 1;
            Order orderToSave = ObjectMother.GetOrderOk(_product);

            Order orderSaved = _repository.Save(orderToSave);

            orderSaved.Id.Should().BeGreaterThan(0);
            orderSaved.Client.Should().Be("José da Silva");
        }

        [Test]
        public void Order_TestRepository_SaveOrder_ClientNameNullOrEmpty_ShouldBeFail()
        {
            int id = 2;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Client = null;

            Action comparison = () => { _repository.Save(orderToSave); };

            comparison.Should().Throw<OrderClientNullOrEmptyException>();
            Order resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Order_TestRepository_SaveOrder_ClientNameOverFlow_ShouldBeFail()
        {
            int id = 2;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Client = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparison = () => { _repository.Save(orderToSave); };

            comparison.Should().Throw<OrderClientNameOverFlowException>();
            Order resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Order_TestRepository_SaveOrder_ClientNameShort_ShouldBeFail()
        {
            int id = 2;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Client = "ABC";

            Action comparison = () => { _repository.Save(orderToSave); };

            comparison.Should().Throw<OrderClientNameShortException>();
            Order resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Order_TestRepository_SaveOrder_AmountZero_ShouldBeFail()
        {
            int id = 2;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.Amount = 0;

            Action comparison = () => { _repository.Save(orderToSave); };

            comparison.Should().Throw<OrderAmountZeroException>();
            Order resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Order_TestRepository_SaveOrder_ProductNull_ShouldBeFail()
        {
            int id = 2;
            Order orderToSave = ObjectMother.GetOrderOk(_product);
            orderToSave.product = null;

            Action comparison = () => { _repository.Save(orderToSave); };

            comparison.Should().Throw<OrderProductNullException>();
            Order resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_ShouldBeOk()
        {
            int id = 1;
            _product.Id = 1;
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Id = 1;
            orderToUpdate.Client = "Teste de Atualização";

            _repository.Update(orderToUpdate);

            Order resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().NotBeNull();
            resultadoEncontrado.Client.Should().Be("Teste de Atualização");
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_InvalidId_ShouldBeFail()
        {
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Id = -1;

            Action comparation = () => _repository.Update(orderToUpdate);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_ClientNameNullOrEmpty_ShouldBeFail()
        {
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Client = "";

            Action comparation = () => _repository.Update(orderToUpdate);

            comparation.Should().Throw<OrderClientNullOrEmptyException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_ClientNameOverFlow_ShouldBeFail()
        {
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Client = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _repository.Update(orderToUpdate);

            comparation.Should().Throw<OrderClientNameOverFlowException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_ClientNameShort_ShouldBeFail()
        {
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Client = "ABC";

            Action comparation = () => _repository.Update(orderToUpdate);

            comparation.Should().Throw<OrderClientNameShortException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_ProductNull_ShouldBeFail()
        {
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.product = null;

            Action comparation = () => _repository.Update(orderToUpdate);

            comparation.Should().Throw<OrderProductNullException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_UpdateOrder_AmountZero_ShouldBeFail()
        {
            Order orderToUpdate = ObjectMother.GetOrderOk(_product);
            orderToUpdate.Amount = 0;

            Action comparation = () => _repository.Update(orderToUpdate);

            comparation.Should().Throw<OrderAmountZeroException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_GetOrder__ShouldBeOk()
        {
            int id = 1;

            Order orderReturns = _repository.Get(id);

            orderReturns.Should().NotBeNull();
            orderReturns.Id.Should().Be(id);
            orderReturns.Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_GetOrder_OrderNotFound__ShouldBeNull()
        {
            int id = 2;

            Order orderReturns = _repository.Get(id);

            orderReturns.Should().BeNull();
        }

        [Test]
        public void Order_TestRepository_GetAll__ShouldBeOk()
        {
            IEnumerable<Order> listOrders = _repository.GetAll();

            listOrders.Count().Should().BeGreaterOrEqualTo(1);
            listOrders.First().Id.Should().Be(1);
            listOrders.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_GetAll__ShouldBeNull()
        {
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = 1;
            _repository.Delete(orderToDelete);

            IEnumerable<Order> listOrders = _repository.GetAll();

            listOrders.Count().Should().Be(0);
        }

        [Test]
        public void Order_TestRepository_DeleteOrder__ShouldBeOk()
        {
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = 1;

            _repository.Delete(orderToDelete);

            IEnumerable<Order> listOrders = _repository.GetAll();
            listOrders.Count().Should().Be(0);
        }

        [Test]
        public void Order_TestRepository_DeleteOrder_InvalidId_ShouldBeFail()
        {
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = -1;

            Action compartation = () => _repository.Delete(orderToDelete);

            compartation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Order_TestRepository_DeleteOrder_OrderNotFound_ShouldBeFail()
        {
            int id = 1;
            Order orderToDelete = ObjectMother.GetOrderOk(_product);
            orderToDelete.Id = 2;

            _repository.Delete(orderToDelete);

            IEnumerable<Order> listOrders = _repository.GetAll();
            listOrders.Count().Should().Be(1);
            listOrders.First().Id.Should().Be(id);
            listOrders.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_GetByProduct_ShouldBeFail()
        {
            int id = 1;
            Order orderToFind = ObjectMother.GetOrderOk(_product);
            orderToFind.Id = 1;
            orderToFind.product.Id = 1;


            IEnumerable<Order> listOrders = _repository.GetByProduct(orderToFind.product.Id);
            listOrders.Count().Should().Be(1);
            listOrders.First().Id.Should().Be(id);
            listOrders.First().Client.Should().Be("Compra de Teste");
        }

        [Test]
        public void Order_TestRepository_GetByProduct_ShouldBeNull()
        {
            Order orderToFind = ObjectMother.GetOrderOk(_product);
            orderToFind.Id = 1;
            orderToFind.product.Id = 2;

            IEnumerable<Order> listOrders = _repository.GetByProduct(orderToFind.product.Id);
            listOrders.Count().Should().Be(0);
        }

        [Test]
        public void Order_TestRepository_GetByProduct_InvalidId_ShouldBeFail()
        {
            Order orderToFind = ObjectMother.GetOrderOk(_product);
            orderToFind.Id = 1;
            orderToFind.product.Id = -2;

            Action comparation = () =>_repository.GetByProduct(orderToFind.product.Id);
            comparation.Should().Throw<IdentifierUndefinedException>();
        }

    }
}
