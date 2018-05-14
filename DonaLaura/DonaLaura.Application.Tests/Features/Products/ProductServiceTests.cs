using DonaLaura.Application.Features.Products;
using DonaLaura.Common.Tests.Base;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Application.Tests.Features.Products
{
    [TestFixture]
    public class ProductServiceTests
    {
        Product _product;
        Product _invalidProduct;
        Mock<IProductRepository> _mockRepository;
        ProductService _servico;

        [SetUp]
        public void Initialize()
        {
            _product = ObjectMother.GetProductOk();
            _mockRepository = new Mock<IProductRepository>();
            _servico = new ProductService(_mockRepository.Object);
        }

        [Test]
        public void Product_TestService_AddProduct_ShouldBeOk()
        {
            _mockRepository.Setup(pr => pr.Save(_product)).Returns(_product);

            Product resultadoEncontrado = _servico.PostAdd(_product);

            _mockRepository.Verify(pr => pr.Save(_product));
            resultadoEncontrado.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Product_TestService_AddProduct_ShouldBeFail()
        {
            _product.Name = "";
            _mockRepository.Setup(pr => pr.Save(_product));

            Action comparation = () => _servico.PostAdd(_product);

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Product_TestService_UpdateProduct_ShouldBeOk()
        {
            _product.Name = "Teste de Atualização";
            _mockRepository.Setup(pr => pr.Update(_product)).Returns(_product);

            Product resultadoEncontrado = _servico.Update(_product);

            _mockRepository.Verify(pr => pr.Update(_product));
            resultadoEncontrado.Id.Should().BeGreaterThan(0);
            resultadoEncontrado.Name.Should().Be("Teste de Atualização");
        }

        [Test]
        public void Product_TestService_UpdateProduct_ShouldBeFail()
        {
            _product.Name = "";
            _mockRepository.Setup(pr => pr.Update(_product));

            Action comparation = () => _servico.Update(_product);

            comparation.Should().Throw<ProductNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Product_TestService_DeleteProduct_ShouldBeOk()
        {
            _mockRepository.Setup(pr => pr.Delete(_product));
            _servico.Delete(_product);
            _mockRepository.Verify(pr => pr.Delete(_product));
        }

        [Test]
        public void Product_TestService_DeleteProduct_ShouldBeFail()
        {
            _product.Id = -1;
            _mockRepository.Setup(pr => pr.Delete(_product));

            Action comparation = () => _servico.Delete(_product);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Product_TestService_GetProduct_ShouldBeOk()
        {
            _mockRepository.Setup(pr => pr.Get(_product.Id)).Returns(_product);

            Product resultadoEncontrado = _servico.Get(_product.Id);

            _mockRepository.Verify(pr => pr.Get(_product.Id));
            resultadoEncontrado.Id.Should().BeGreaterThan(0);
            resultadoEncontrado.Id.Should().Be(_product.Id);
        }

        [Test]
        public void Product_TestService_GetProduct_ShouldBeNull()
        {
            _mockRepository.Setup(pr => pr.Get(_product.Id));

            var listPosts = _servico.Get(_product.Id);

            listPosts.Should().BeNull();
            _mockRepository.Verify(psv => psv.Get(_product.Id));
        }

        [Test]
        public void Product_TestService_GetProduct_ShouldBeFail()
        {
            _product.Id = -1;
            _mockRepository.Setup(pr => pr.Get(_product.Id));

            Action comparation = () => _servico.Get(_product.Id);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Product_TestService_GetAllProducts_ShouldBeOk()
        {
            _mockRepository.Setup(pr => pr.GetAll()).Returns(new List<Product> { _product });

            IEnumerable<Product> resultadoEncontrado = _servico.GetAll();

            resultadoEncontrado.Should().BeEquivalentTo(new List<Product> { _product });
            _mockRepository.Verify(pr => pr.GetAll());
        }

        [Test]
        public void Product_TestService_GetAllProducts_ShouldBeNull()
        {
            _mockRepository.Setup(pr => pr.GetAll());

            var listPosts = _servico.GetAll();

            listPosts.Should().BeNull();
            _mockRepository.Verify(psv => psv.GetAll());
        }
    }
}
