using NDD.Nfe.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Common.Tests.Base
{
    public static class BaseSqlTests
    {
        private const string INSERT_FIRST_STUDENT = "INSERT INTO student (age,name,average) VALUES (18,'joão',9.34)";
        private const string INSERT_SECOND_STUDENT = "INSERT INTO student (age,name,average) VALUES (18,'joão',9.74)";
        private const string INSERT_STUDENT_WHITOUT_DEPENDENCES = "INSERT INTO student (age,name,average) VALUES (18,'pedro',9.75)";

        private const string INSERT_EVALUATION = "INSERT INTO evaluation (assunto,date) VALUES ('teste de assunto 1', GETDATE())";
        private const string INSERT_EVALUATION_WHITOUT_DEPENDENCES = "INSERT INTO evaluation (assunto,date) VALUES ('teste de assunto2', GETDATE())";

        private const string INSERT_FIRST_RESULT = "INSERT INTO result (note,evaluation_id,student_id) VALUES (10.0,1,1)";
        private const string INSERT_SECOND_RESULT = "INSERT INTO result (note,evaluation_id,student_id) VALUES (8.0,3,2)";
        private const string INSERT_THIRD_RESULT = "INSERT INTO result (note,evaluation_id,student_id) VALUES (10.0,2,2)";

        private const string RECREASE_TABLE_STUDENT = "DELETE FROM student DBCC CHECKIDENT ('student',RESEED,0)";
        private const string RECREASE_TABLE_EVALUATION = "DELETE FROM evaluation DBCC CHECKIDENT ('evaluation',RESEED,0)";
        private const string RECREASE_TABLE_RESULT = "DELETE FROM result DBCC CHECKIDENT ('result',RESEED,0)";

        public static void SeedDatabase()
        {
            Db.Update(RECREASE_TABLE_RESULT);
            Db.Update(RECREASE_TABLE_EVALUATION);
            Db.Update(RECREASE_TABLE_STUDENT);

            Db.Update(INSERT_FIRST_STUDENT);
            Db.Update(INSERT_STUDENT_WHITOUT_DEPENDENCES);
            Db.Update(INSERT_SECOND_STUDENT);

            Db.Update(INSERT_EVALUATION);
            Db.Update(INSERT_EVALUATION);
            Db.Update(INSERT_EVALUATION);
            Db.Update(INSERT_EVALUATION_WHITOUT_DEPENDENCES);

            Db.Update(INSERT_FIRST_RESULT);
            Db.Update(INSERT_SECOND_RESULT);
            Db.Update(INSERT_THIRD_RESULT);

        }
    }
}
