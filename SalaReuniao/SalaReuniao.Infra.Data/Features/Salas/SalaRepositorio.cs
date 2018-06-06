using DonaLaura.Infra;
using SalaReuniao.Domain.Common;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Infra.Data.Features.Salas
{
    public class SalaRepositorio : ISalaRepositorio
    {
        #region#Scripts
        private const string _inserir = "INSERT INTO TBSala (nome_sala, numero_lugares) " +
                                    "VALUES (@Nome, @NumeroLugares)";

        private const string _carregarPorId = "SELECT * FROM TBSala WHERE id_sala = @IdSala";

        private const string _atualizar = "UPDATE TBSala SET nome_sala = @Nome, " +
                                                            "numero_lugares = @NumeroLugares " +
                                                            "WHERE id_sala = @IdSala";

        private const string _carregarTodos = "SELECT * FROM TBSala";

        private const string _deletar = "DELETE FROM TBSala WHERE id_sala = @IdSala";

        #endregion#Scripts

        public void Deletar(Sala sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            Db.Delete(_deletar, new object[] { "@IdSala", sala.Id });
        }

        public Sala Carregar(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_carregarPorId, Make, new object[] { "@IdSala ", id });

        }

        public IEnumerable<Sala> CarregarTodos()
        {
            return Db.GetAll(_carregarTodos, Make);
        }

        public Sala Salvar(Sala sala)
        {
            sala.Validar();
            sala.Id = Db.Insert(_inserir, Take(sala));

            return sala;
        }

        public Sala Atualizar(Sala sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();

            Db.Update(_atualizar, Take(sala));

            return sala;
        }

        private static Func<IDataReader, Sala> Make = reader =>
           new Sala
           {
               Id = Convert.ToInt64(reader["id_sala"]),
               Nome = (Nome)Enum.Parse(typeof(Nome), Convert.ToString(reader["nome_sala"])),
               NumeroLugares = Convert.ToInt32(reader["numero_lugares"])
           };

        private object[] Take(Sala sala)
        {
            return new object[]
            {
                "@IdSala", sala.Id,
                "@Nome", sala.Nome,
                "@NumeroLugares", sala.NumeroLugares

            };
        }
    }
}
