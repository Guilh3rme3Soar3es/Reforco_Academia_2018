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
        private const string RECRIAR_TBEVENTO = "DELETE FROM [dbo].[TBEvento] DBCC CHECKIDENT('TBEvento', RESEED, 0)";
        private const string RECRIAR_TBSALA = "DELETE FROM [dbo].[TBSala] DBCC CHECKIDENT('TBSala', RESEED, 0)";
        private const string RECRIAR_TBFUINCIONARIO = "DELETE FROM [dbo].[TBFuncionario] DBCC CHECKIDENT('TBFuncionario', RESEED, 0)";

        private const string INSERIR_SALA = "INSERT INTO TBSala (nome,numero_lugares) VALUES ('TREINAMENTO', 32)";
        private const string INSERIR_SALA_SEMDEPENDENCIAS = "INSERT INTO TBSala (nome,numero_lugares) VALUES ('REUNIAO', 32)";

        private const string INSERIR_FUNCIONARIO = "INSERT INTO TBFuncionario (nome,cargo,ramal) VALUES ('João da Silva', 'Diretor', '1234')";
        private const string INSERIR_FUNCIONARIO_SEM_DEPENDENCIAS = "INSERT INTO TBFuncionario (nome,cargo,ramal) VALUES ('Pedro Pafuncio', 'Gerentre', '7894')";

        private const string INSERIR_EVENTO = "INSERT INTO TBEvento (data_inicio,data_termino,funcionario_id,sala_id) VALUES (GETDATE(),GETDATE(),2,2)";

        public static void POPULAR_BANCO()
        {
            Db.Update(RECRIAR_TBEVENTO);
            Db.Update(RECRIAR_TBSALA);
            Db.Update(RECRIAR_TBFUINCIONARIO);

            Db.Update(INSERIR_SALA);
            Db.Update(INSERIR_SALA_SEMDEPENDENCIAS);

            Db.Update(INSERIR_FUNCIONARIO);
            Db.Update(INSERIR_FUNCIONARIO_SEM_DEPENDENCIAS);

            Db.Update(INSERIR_EVENTO);
        }
    }
}
