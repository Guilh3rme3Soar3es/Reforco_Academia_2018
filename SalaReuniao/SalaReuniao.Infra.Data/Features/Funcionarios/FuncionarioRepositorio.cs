using DonaLaura.Infra;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Infra.Data.Features.Funcionarios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        #region#Scripts
        private const string _inserir = "INSERT INTO TBFuncionario (nome, cargo,ramal) " +
                                    "VALUES (@Nome, @Cargo, @Ramal)";

        private const string _carregarPorId = "SELECT * FROM TBFuncionario WHERE id_funcionario = @IdFuncionario";

        private const string _atualizar = "UPDATE TBFuncionario SET nome = @Nome, " +
                                                            "cargo = @Cargo, ramal = @Ramal " +
                                                            "WHERE id_funcionario = @IdFuncionario";

        private const string _carregarTodos = "SELECT * FROM TBFuncionario";

        private const string _deletar = "DELETE FROM TBFuncionario WHERE id_funcionario = @IdFuncionario";

        #endregion#Scripts

        public void Deletar(Funcionario funcionario)
        {
            if (funcionario.Id <= 0)
                throw new IdentifierUndefinedException();
            Db.Delete(_deletar, new object[] { "@IdFuncionario", funcionario.Id });
        }

        public Funcionario Carregar(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_carregarPorId, Make, new object[] { "@IdFuncionario ", id });

        }

        public IEnumerable<Funcionario> CarregarTodos()
        {
            return Db.GetAll(_carregarTodos, Make);
        }

        public Funcionario Salvar(Funcionario funcionario)
        {
            funcionario.Validar();
            funcionario.Id = Db.Insert(_inserir, Take(funcionario));

            return funcionario;
        }

        public Funcionario Atualizar(Funcionario funcionario)
        {
            if (funcionario.Id <= 0)
                throw new IdentifierUndefinedException();
            funcionario.Validar();

            Db.Update(_atualizar, Take(funcionario));

            return funcionario;
        }

        private static Func<IDataReader, Funcionario> Make = reader =>
           new Funcionario
           {
               Id = Convert.ToInt64(reader["id_funcionario"]),
               Nome = Convert.ToString(reader["nome"]),
               Cargo = Convert.ToString(reader["cargo"]),
               Ramal  = Convert.ToString(reader["ramal"])
           };

        private object[] Take(Funcionario funcionario)
        {
            return new object[]
            {
                "@IdFuncionario", funcionario.Id,
                "@Nome", funcionario.Nome,
                "@Cargo", funcionario.Cargo,
                "@Ramal", funcionario.Ramal
            };
        }
    }
}
