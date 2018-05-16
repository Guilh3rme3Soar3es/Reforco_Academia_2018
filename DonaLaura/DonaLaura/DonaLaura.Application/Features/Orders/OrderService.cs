using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Orders;

namespace DonaLaura.Application.Features.Orders
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void Delete(Order order)
        {
            try
            {
                if (order.Id <= 0)
                    throw new IdentifierUndefinedException();
                _orderRepository.Delete(order);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Order Get(long id)
        {
            try
            {
                if (id <= 0)
                    throw new IdentifierUndefinedException();
                return _orderRepository.Get(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Order> GetAll()
        {
            try
            {
                return _orderRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Order Add(Order order)
        {
            try
            {
                order.Validate();
                return _orderRepository.Save(order);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Order Update(Order order)
        {
            try
            {
                order.Validate();
                return _orderRepository.Update(order);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Order> GetByProduct(long idProduct)
        {
            try
            {
                if (idProduct <= 0)
                    throw new IdentifierUndefinedException();
                return _orderRepository.GetByProduct(idProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
