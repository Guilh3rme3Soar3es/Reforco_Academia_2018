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
        private const string RECREATE_POST_TABLE = "TRUNCATE TABLE [dbo].[Products] ";
        private const string INSERT_POST = "INSERT INTO Products(Message,PostDate) VALUES ('Post de Teste', GETDATE())";//arruamar

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_POST_TABLE);
            Db.Update(INSERT_POST);
        }
    }
}

