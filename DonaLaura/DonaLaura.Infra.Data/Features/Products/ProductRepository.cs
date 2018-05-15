using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Features.Products
{
    public class ProductRepository : IProductRepository
    {
        #region#Scripts
        private const string _insert = "INSERT INTO TBProduct (Name, CostPrice, SalePrice, Fabrication, Expiration, IsAvaliable) " +
                                    "VALUES (@Name, @CostPrice, @SalePrice, @Fabrication, @Expiration, @IsAvaliable)";

        private const string _getById = "SELECT * FROM TBProduct WHERE IdProduct = @IdProduct";

        private const string _update = "UPDATE TBProduct SET Name = @Name, " +
                                                            "CostPrice = @CostPrice, " +
                                                            "SalePrice = @SalePrice, " +
                                                            "Fabrication = @Fabrication, " +
                                                            "Expiration = @Expiration, " +
                                                            "IsAvaliable = @IsAvaliable WHERE IdProduct = @IdProduct";

        private const string _getAll = "SELECT * FROM TBProduct";

        private const string _delete = "DELETE FROM TBProduct WHERE IdProduct = @IdProduct";

        #endregion#Scripts
        public void Delete(Product product)
        {
            if (product.Id < 0)
                throw new IdentifierUndefinedException();
            Db.Delete(_delete, new object[] { "@IdProduct", product.Id });
        }

        public Product Get(long id)
        {
            return Db.Get(_getById, Make, new object[] { "@IdProduct ", id });
        }

        public IEnumerable<Product> GetAll()
        {
            return Db.GetAll(_getAll, Make);
        }

        public Product Save(Product product)
        {
            product.Validate();
            product.Id = Db.Insert(_insert, Take(product));

            return product;
        }

        public Product Update(Product product)
        {
            if (product.Id < 0)
                throw new IdentifierUndefinedException();
            product.Validate();

            Db.Update(_update, Take(product));

            return product;
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Product> Make = reader =>
           new Product
           {
               Id = Convert.ToInt64(reader["IdProduct"]),
               Name = reader["Name"].ToString(),
               CostPrice = Convert.ToDouble(reader["CostPrice"]),
               SalePrice = Convert.ToDouble(reader["SalePrice"]),
               Manufacture = Convert.ToDateTime(reader["Fabrication"]),
               Expiration = Convert.ToDateTime(reader["Expiration"]),
               IsAvaliable = Convert.ToBoolean(reader["IsAvaliable"])

           };


        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="post">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Product product)
        {
            return new object[]
            {
                "@IdProduct", product.Id,
                "@Name", product.Name,
                "@CostPrice", product.CostPrice,
                "@SalePrice", product.SalePrice,
                "@Fabrication", product.Manufacture,
                "@Expiration", product.Manufacture,
                "@IsAvaliable" , product.IsAvaliable

            };
        }
    }
}
