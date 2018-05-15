using DonaLaura.Domain.Features.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Application.Features.Orders
{
    public interface IOrderService
    {
        Order PostAdd(Order order);
        Order Update(Order order);
        Order Get(long id);
        IEnumerable<Order> GetAll();
        void Delete(Order order);

        IEnumerable<Order> GetByProduct(int idProduct);
    }
}
