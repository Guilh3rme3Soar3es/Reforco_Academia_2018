using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Application.Features.Orders;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;

namespace DonaLaura.Application.Features.Products
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        private IOrderService _orderService;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public ProductService(IProductRepository repository, IOrderService orderService)
        {
            _repository = repository;
            _orderService = orderService;
        }
        public void Delete(Product product)
        {
            try
            {
                if (_orderService.GetByProduct(product.Id).Count() > 0)
                    throw new ProductWithDependecesException();
                if (product.Id < 0)
                    throw new IdentifierUndefinedException();
                _repository.Delete(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product Get(long id)
        {
            try
            {
                if (id < 0)
                    throw new IdentifierUndefinedException();
                return _repository.Get(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product Add(Product product)
        {
            try
            {
                product.Validate();
                return _repository.Save(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product Update(Product product)
        {
            try
            {
                product.Validate();
                return _repository.Update(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
