using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Common.Tests.Features.ObjectMother;
using DonaLaura.Infra.Data.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Tests.Features.Products
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductRepository _repository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();

            _repository = new ProductRepository();
        }

        [Test]
        public void Product_TestRepository_SaveProduct_ShouldBeOk()
        {
            Product productToSave = ObjectMother.GetProductOk();

            Product productSaved = _repository.Save(productToSave);

            productSaved.Id.Should().BeGreaterThan(0);
            productSaved.Name.Should().Be("Produto Ok");
        }

        [Test]
        public void Product_TestRepository_SaveProduct_NameNullOrEmpty_ShouldBeFail()
        {
            int id = 2;
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Name = null;

            Action comparison = () => { _repository.Save(productToSave); };

            comparison.Should().Throw<ProductNameNullOrEmptyException>();
            Product resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Product_TestRepository_SaveProduct_NameOverFlow_ShouldBeFail()
        {
            int id = 2;
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparison = () => { _repository.Save(productToSave); };

            comparison.Should().Throw<ProductNameOverFlowException>();
            Product resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Product_TestRepository_SaveProduct_CostPriceOverFlow_ShouldBeFail()
        {
            int id = 2;
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.CostPrice = 2.00;
            productToSave.SalePrice = 1.00;

            Action comparison = () => { _repository.Save(productToSave); };

            comparison.Should().Throw<ProductCostPriceOverFlow>();
            Product resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Product_TestRepository_SaveProduct_ExpirationInvalid_ShouldBeFail()
        {
            int id = 2;
            Product productToSave = ObjectMother.GetProductOk();
            productToSave.Manufacture = DateTime.Now.AddDays(+1);
            productToSave.Expiration = DateTime.Now.AddDays(-2);

            Action comparation = () => _repository.Save(productToSave);

            comparation.Should().Throw<ProductExpirationInvalidException>();
            Product resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().BeNull();
        }

        [Test]
        public void Product_TestRepository_UpdateProduct_ShouldBeOk()
        {
            int id = 1;
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Id = 1;
            productToUpdate.Name = "Teste de Atualização";

            _repository.Update(productToUpdate);

            Product resultadoEncontrado = _repository.Get(id);
            resultadoEncontrado.Should().NotBeNull();
            resultadoEncontrado.Name.Should().Be("Teste de Atualização");
        }

        [Test]
        public void Product_TestRepository_UpdateProduct_InvalidId_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Id = -1;

            Action comparation = () => _repository.Update(productToUpdate);

            comparation.Should().Throw<IdentifierUndefinedException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_UpdateProduct_NameNullOrEmpty_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Name = "";

            Action comparation = () => _repository.Update(productToUpdate);

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_UpdateProduct_NameOverFlow_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";

            Action comparation = () => _repository.Update(productToUpdate);

            comparation.Should().Throw<ProductNameOverFlowException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_UpdateProduct_CostPriceOverFlow_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.CostPrice = 2.00;
            productToUpdate.SalePrice = 1.00;

            Action comparation = () => _repository.Update(productToUpdate);

            comparation.Should().Throw<ProductCostPriceOverFlow>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_UpdateProduct_ExpirationInvalid_ShouldBeFail()
        {
            Product productToUpdate = ObjectMother.GetProductOk();
            productToUpdate.Manufacture = DateTime.Now.AddDays(+1);
            productToUpdate.Expiration = DateTime.Now.AddDays(-2);

            Action comparation = () => _repository.Update(productToUpdate);

            comparation.Should().Throw<ProductExpirationInvalidException>();
            var listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_GetProduct__ShouldBeOk()
        {
            int id = 1;

            Product productReturns = _repository.Get(id);

            productReturns.Should().NotBeNull();
            productReturns.Id.Should().Be(id);
            productReturns.Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_GetProduct_ProductNotFound__ShouldBeNull()
        {
            int id = 2;

            Product productReturns = _repository.Get(id);

            productReturns.Should().BeNull();
        }

        [Test]
        public void Product_TestRepository_GetProduct_InvalidId__ShouldBeNull()
        {
            int id = -2;

            Action comparation = () => _repository.Get(id);

            comparation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Product_TestRepository_GetAll__ShouldBeOk()
        {
            IEnumerable<Product> listProducts = _repository.GetAll();

            listProducts.Count().Should().BeGreaterOrEqualTo(1);
            listProducts.First().Id.Should().Be(1);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

        [Test]
        public void Product_TestRepository_GetAll__ShouldBeNull()
        {
            BaseSqlTests.Helper_ClearDataBase();

            IEnumerable<Product> listProducts = _repository.GetAll();

            listProducts.Count().Should().Be(0);
        }

        [Test]
        public void Product_TestRepository_DeleteProduct__ShouldBeOk()
        {
            BaseSqlTests.Helper_ClearTBOrder();
            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = 1;

            _repository.Delete(productToDelete);

            IEnumerable<Product> listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(0);
        }

        [Test]
        public void Product_TestRepository_DeleteProduct_InvalidId_ShouldBeFail()
        {
            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = -1;

            Action compartation = () => _repository.Delete(productToDelete);

            compartation.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Product_TestRepository_DeleteProduct_ProductNotFound_ShouldBeFail()
        {
            int id = 1;
            Product productToDelete = ObjectMother.GetProductOk();
            productToDelete.Id = 2;

            _repository.Delete(productToDelete);

            IEnumerable<Product> listProducts = _repository.GetAll();
            listProducts.Count().Should().Be(1);
            listProducts.First().Id.Should().Be(id);
            listProducts.First().Name.Should().Be("Produto de Teste");
        }

    }
}

