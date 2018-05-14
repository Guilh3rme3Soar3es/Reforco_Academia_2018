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
        private const string RECREATE_POST_TABLE = "TRUNCATE TABLE [dbo].[TBProduct]";
        private const string INSERT_POST = "INSERT INTO TBProduct(Name,CostPrice,SalePrice,Fabrication,Expiration,IsAvaliable) VALUES ('Produto de Teste', 1.00, 2.00, GETDATE(), GETDATE(), 0)";//arruamar

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_POST_TABLE);
            Db.Update(INSERT_POST);
        }
    }
}

