using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Orders;
using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Features.Orders
{
    public class OrderRepository : IOrderRepository
    {
        #region#Scripts

        private readonly string _insert = "INSERT INTO TBOrder (Cliente, Lucro,Quantidade, ProductId) VALUES (@Cliente,@Lucro,@Quantidade, @IdProduct)";

        private const string _getById = "SELECT o.*, p.Name AS product_name, p.CostPrice AS product_CostPrice, p.SalePrice AS product_SalePrice, p.Fabrication AS product_Fabrication, p.Expiration AS product_Expiration, p.IsAvaliable AS product_IsAvaliable FROM TBOrder AS o INNER JOIN TBProduct AS p ON (o.ProductId = p.IdProduct) WHERE o.IdOrder = @IdOrder";

        private const string _update = "UPDATE TBOrder SET Cliente = @Cliente, " +
                                                           "Lucro = @Lucro," +
                                                           "Quantidade = @Quantidade," +
                                                           "ProductId = @IdProduct" +
                                                           " WHERE IdOrder = @IdOrder";

        private const string _getAll = "SELECT * FROM TBProduct";

        private const string _getAllTeste = "SELECT o.*, p.Name AS product_name, p.CostPrice AS product_CostPrice, p.SalePrice AS product_SalePrice, p.Fabrication AS product_Fabrication, p.Expiration AS product_Expiration, p.IsAvaliable AS product_IsAvaliable FROM TBOrder AS o INNER JOIN TBProduct AS p ON (o.ProductId = p.IdProduct)";

        private const string _delete = "DELETE FROM TBOrder WHERE IdOrder = @IdOrder";

        private const string _getByProduct = "SELECT o.*, p.Name AS product_name, p.CostPrice AS product_CostPrice, p.SalePrice AS product_SalePrice, p.Fabrication AS product_Fabrication, p.Expiration AS product_Expiration, p.IsAvaliable AS product_IsAvaliable FROM TBOrder AS o INNER JOIN TBProduct AS p ON (o.ProductId = p.IdProduct) WHERE o.ProductId = @IdProduct";

        #endregion#Scripts
        public void Delete(Order order)
        {
            try
            {
                if (order.Id <= 0)
                    throw new IdentifierUndefinedException();
                Db.Delete(_delete, new object[] { "@IdOrder", order.Id });
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
                return Db.Get(_getById, Make, new object[] { "@IdOrder ", id });

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
                return Db.GetAll(_getAllTeste, Make);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Order Save(Order order)
        {
            try
            {
                order.Validate();
                order.Id = Db.Insert(_insert, Take(order));

                return order;
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
                if (order.Id <= 0)
                    throw new IdentifierUndefinedException();

                order.Validate();

                Db.Update(_update, Take(order));

                return order;
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
                return Db.GetAll(_getByProduct, Make, new object[] { "@IdProduct ", idProduct });
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static Func<IDataReader, Order> Make = reader =>
           new Order
           {
               Id = Convert.ToInt64(reader["IdOrder"]),
               Client = Convert.ToString(reader["Cliente "]),
               Profit = Convert.ToDouble(reader["Lucro"]),
               Amount = Convert.ToInt32(reader["Quantidade"]),
               product = new Product()
               {
                   Id = Convert.ToInt32(reader["ProductId"]),
                   Name = Convert.ToString(reader["product_name"]),
                   CostPrice = Convert.ToDouble(reader["product_CostPrice"]),
                   SalePrice = Convert.ToDouble(reader["product_SalePrice"]),
                   Manufacture = Convert.ToDateTime(reader["product_Fabrication"]),
                   Expiration = Convert.ToDateTime(reader["product_Expiration"]),
                   IsAvaliable = Convert.ToBoolean(reader["product_IsAvaliable"])
               }

           };

        private object[] Take(Order order)
        {
            return new object[]
            {
                "@IdOrder", order.Id,
                "@Cliente", order.Client,
                "@Lucro", order.Profit,
                "@Quantidade", order.Amount,
                "@IdProduct", order.product.Id
            };
        }
    }
}
