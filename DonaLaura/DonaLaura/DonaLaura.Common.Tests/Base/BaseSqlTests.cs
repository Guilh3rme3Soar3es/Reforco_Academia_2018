using DonaLaura.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Common.Tests.Base
{
    public static class BaseSqlTests
    {
        private const string RECREATE_POST_TABLE = "DELETE FROM [dbo].[TBProduct] DBCC CHECKIDENT('TBProduct', RESEED, 0)";
        private const string RECREATE_ORDER_TABLE = "DELETE FROM [dbo].[TBOrder] DBCC CHECKIDENT('TBOrder', RESEED, 0)";
        private const string INSERT_ORDER = "INSERT INTO TBOrder (Cliente, Lucro,Quantidade,ProductId) VALUES ('Compra de Teste', 1.00, 1, 1)";
        private const string INSERT_PRODUCT = "INSERT INTO TBProduct(Name,CostPrice,SalePrice,Fabrication,Expiration,IsAvaliable) VALUES ('Produto de Teste', 1.00, 2.00, GETDATE(), GETDATE(), 0)";//arruamar

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_ORDER_TABLE);
            Db.Update(RECREATE_POST_TABLE);
            Db.Update(INSERT_PRODUCT);
            Db.Update(INSERT_ORDER);
        }

        public static void Helper_ClearDataBase()
        {
            Db.Update(RECREATE_ORDER_TABLE);
            Db.Update(RECREATE_POST_TABLE);
        }

        public static void Helper_ClearTBOrder()
        {
            Db.Update(RECREATE_ORDER_TABLE);
        }
    }
}

