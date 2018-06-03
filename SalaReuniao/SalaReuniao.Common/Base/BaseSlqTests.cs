using DonaLaura.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Common.Base
{
    public static class BaseSqlTests
    {
        private const string RECREATE_BOOK_TABLE = "DELETE FROM [dbo].[TBBook] DBCC CHECKIDENT('TBBook', RESEED, 0)";
        private const string RECREATE_LOAN_TABLE = "DELETE FROM [dbo].[TBLoan] DBCC CHECKIDENT('TBLoan', RESEED, 0)";
        private const string INSERT_LOAN = "INSERT INTO TBLoan (NameClient, DateDevolution,BookId) VALUES ('Emprestimo de Teste', GETDATE(), 1)";
        private const string INSERT_BOOK = "INSERT INTO TBBook(Title,Theme,Author,Volume,DatePost,IsAvaliable) VALUES " +
                                                                                                                                "('Livro de Teste', 'Testes Automatizados', 'Eu mesmo', 1, GETDATE(), 0)";

        public static void SeedDatabase()
        {
            Db.Update(RECREATE_LOAN_TABLE);
            Db.Update(RECREATE_BOOK_TABLE);
            Db.Update(INSERT_BOOK);
            Db.Update(INSERT_LOAN);
        }

        public static void ClearDataBase()
        {
            Db.Update(RECREATE_LOAN_TABLE);
            Db.Update(RECREATE_BOOK_TABLE);
        }

        public static void ClearTBLoan()
        {
            Db.Update(RECREATE_LOAN_TABLE);
        }
    }
}
