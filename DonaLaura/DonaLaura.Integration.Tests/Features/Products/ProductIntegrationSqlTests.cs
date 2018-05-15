using DonaLaura.Application.Features.Orders;
using DonaLaura.Application.Features.Products;
using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Orders;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Infra.Data.Features.Orders;
using DonaLaura.Infra.Data.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Integration.Tests.Features.Products
{
    [TestFixture]
    public class ProductIntegrationSqlTests
    {
        private ProductService _productService;
        private OrderService _orderService;
        private ProductRepository _repository;
        private OrderRepository _orderRepositopty;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();

            _orderRepositopty = new OrderRepository();
            _repository = new ProductRepository();
            _orderService = new OrderService(_orderRepositopty);
            _productService = new ProductService(_repository, _orderService);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_SaveProduct_ShouldBeOk()
        {
            Product productToSave = ObjectMother.GetProductOk();

            Product productSaved = _productService.Add(productToSave);

            Product resultadoEncontrado = _productService.Get(productSaved.Id);
            productSaved.Should().NotBeNull();
            resultadoEncontrado.Id.Should().Be(productSaved.Id);
            productSaved.Name.Should().Be(productToSave.Name);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_SaveProduct_NameNullOrEmpty_ShouldBeFail()
        {
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Name = null;

            Action comparation = () => _productService.Add(productToSave);

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_SaveProduct_NameShort_ShouldBeFail()
        {
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Name = "ABC";

            Action comparation = () => _productService.Add(productToSave);

            comparation.Should().Throw<ProductNameShortException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_SaveProduct_NameOverFlow_ShouldBeFail()
        {
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _productService.Add(productToSave);

            comparation.Should().Throw<ProductNameOverFlowException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_SaveProduct_CostPriceOverFlow_ShouldBeFail()
        {
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.CostPrice = 2.00;
            productToSave.SalePrice = 1.00;

            Action comparation = () => _productService.Add(productToSave);

            comparation.Should().Throw<ProductCostPriceOverFlow>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_SaveProduct_InvalidExpiration_ShouldBeFail()
        {
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Manufacture = DateTime.Now.AddDays(+1);
            productToSave.Expiration = DateTime.Now.AddDays(-1);

            Action comparation = () => _productService.Add(productToSave);

            comparation.Should().Throw<ProductExpirationInvalidException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_ShouldBeOk()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Id = 1;
            productToUpdate.Name = "teste";

            Product productEdited = _productService.Update(productToUpdate);

            productEdited.Should().NotBeNull();
            productEdited.Name.Should().Be(productToUpdate.Name);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_InvalidId_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Id = -1;

            Action comparation = () => _productService.Update(productToUpdate);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_NameNullOrEmpty_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Name = null;

            Action comparation = () => _productService.Update(productToUpdate);

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_NameShort_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Name = "ABC";

            Action comparation = () => _productService.Update(productToUpdate);

            comparation.Should().Throw<ProductNameShortException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_NameOverFlow_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _productService.Update(productToUpdate);

            comparation.Should().Throw<ProductNameOverFlowException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_CostPriceOverFlow_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.CostPrice = 2.00;
            productToUpdate.SalePrice = 1.00;

            Action comparation = () => _productService.Update(productToUpdate);

            comparation.Should().Throw<ProductCostPriceOverFlow>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_UpdateProduct_InvalidExpiration_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Manufacture = DateTime.Now.AddDays(+1);
            productToUpdate.Expiration = DateTime.Now.AddDays(-1);

            Action comparation = () => _productService.Update(productToUpdate);

            comparation.Should().Throw<ProductExpirationInvalidException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_DeleteProduct__ShouldBeOk()
        {
            BaseSqlTests.Helper_RemoveOrder();

            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = 1;

            _productService.Delete(productToDelete);

            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(0);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_DeleteProduct_InvalidId_ShouldBeFail()
        {
            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = -1;

            Action comparation = () => _productService.Delete(productToDelete);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var listPosts = _productService.GetAll();
            listPosts.Count().Should().Be(1);
            listPosts.First().Id.Should().Be(1);
            listPosts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestSystemIntegrationSql_DeleteProduct_ProductNotFound_ShouldBeFail()
        {
            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = 2;

            _productService.Delete(productToDelete);

            var listPosts = _productService.GetAll();
            listPosts.Count().Should().Be(1);
            listPosts.First().Id.Should().Be(1);
            listPosts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestSystemIntegrationSql_GetAll__ShouldBeOk()
        {
            var listPosts = _productService.GetAll();

            listPosts.Should().NotBeNull();
            listPosts.Count().Should().Be(1);
            listPosts.First().Id.Should().Be(1);
            listPosts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestSystemIntegrationSql_GetAll__ShouldBeNull()
        {
            BaseSqlTests.Helper_RemoveOrder();
            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = 1;
            _productService.Delete(productToDelete);

            var listPosts = _productService.GetAll();

            listPosts.Count().Should().Be(0);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_GetProduct__ShouldBeOk()
        {
            var id = 1;
            Product productReturns = _productService.Get(id);

            productReturns.Should().NotBeNull();
            productReturns.Id.Should().Be(id);
            productReturns.Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestSystemIntegrationSql_GetProduct_InvalidId_ShouldBeFail()
        {
            var id = -1;
            Action comparation = () => _productService.Get(id);

            comparation.Should().Throw<IdentifierUndefinedException>();

        }

        [Test]
        public void Product_TestSystemIntegrationSql_GetProduct__ShouldBeNull()
        {
            var id = 2;
            Product productReturns = _productService.Get(id);

            productReturns.Should().BeNull();
        }


        //Conferir com Guilherme ou Thiago...
        [Test]
        public void Product_TestSystemIntegrationSql_GetByProduct_ProductWithDependences_ShouldBeFail()
        {
            Product product = ObjectMother.GetProductOk();
            product.Id = 1;
 
            Action comparation = () => _productService.Delete(product);

            comparation.Should().Throw<ProductWithDependecesException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }

        [Test]
        public void Product_TestSystemIntegrationSql_GetByProduct_InvalidId_ShouldBeFail()
        {
            Product product = ObjectMother.GetProductOk();
            product.Id = -1;

            Action comparation = () => _productService.Delete(product);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var productRetorns = _productService.GetAll();
            productRetorns.Count().Should().Be(1);
        }
    }
}