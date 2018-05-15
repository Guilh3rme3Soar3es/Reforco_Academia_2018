using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public interface IOrderRepository
    {
        Order Save(Order order);
        Order Update(Order order);
        Order Get(long id);
        IEnumerable<Order> GetAll();
        void Delete(Order order);

        IEnumerable<Order> GetByProduct(long idProduct);
    }
}
