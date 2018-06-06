using DonaLaura.Infra;
using SalaReuniao.Domain.Common;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Infra.Data.Features.Eventos
{
    public class EventoRepositorio : IEventoRepositorio
    {
        #region#Scripts
        private const string _inserir = "INSERT INTO TBEvento (data_inicio, data_termino, funcionario_id,sala_id) " +
                                    "VALUES (@Data_Inicio, @Data_Termino, @Funcionario_Id,@Sala_Id)";

        private const string _carregarPorId = "SELECT * FROM TBEvento INNER JOIN TBFuncionario ON funcionario_id = id_funcionario INNER JOIN TBSala ON sala_id = id_sala WHERE id_evento = @IdEvento";

        //teste...
        private const string _carregarPorHorario = "SELECT * FROM TBEvento INNER JOIN TBFuncionario ON funcionario_id = id_funcionario " +
                                                                          "INNER JOIN TBSala ON sala_id = id_sala" +
                                                    " WHERE sala_id = @SalaId AND data_inicio = @Data_inicio " +
                                                    "OR sala_id = @SalaId AND data_inicio BETWEEN @Data_inicio AND @Data_Termino " +
                                                    "OR sala_id = @SalaId AND data_termino BETWEEN @Data_termino AND @Data_Termino";

        private const string _atualizar = "UPDATE TBEvento SET data_inicio = @Data_Inicio, " +
                                                            "data_termino = @Data_Termino," +
                                                            "funcionario_id = @Funcionario_Id," +
                                                            "sala_id = @Sala_Id " +
                                                            "WHERE id_evento = @IdEvento";

        private const string _carregarTodos = "SELECT * FROM TBEvento INNER JOIN TBFuncionario ON funcionario_id = id_funcionario INNER JOIN TBSala ON sala_id = id_sala";

        private const string _deletar = "DELETE FROM TBEvento WHERE id_evento = @IdEvento";

        private const string _carregarPorFuncionario = "SELECT * FROM TBEvento INNER JOIN TBFuncionario ON funcionario_id = id_funcionario INNER JOIN TBSala ON sala_id = id_sala WHERE funcionario_id = @IdFuncionario";

        private const string _carregarPorSala = "SELECT * FROM TBEvento INNER JOIN TBFuncionario ON funcionario_id = id_funcionario INNER JOIN TBSala ON sala_id = id_sala WHERE sala_id = @IdSala";

        #endregion#Scripts

        public void Deletar(Evento evento)
        {
            if (evento.Id <= 0)
                throw new IdentifierUndefinedException();
            Db.Delete(_deletar, new object[] { "@IdEvento", evento.Id });
        }

        public Evento Carregar(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_carregarPorId, Make, new object[] { "@IdEvento ", id });
        }

        public IEnumerable<Evento> CarregarTodos()
        {
            return Db.GetAll(_carregarTodos, Make);
        }

        public IEnumerable<Evento> CarregarPorHorario(Evento evento)
        {
            if (evento.Sala.Id <= 0)
                throw new IdentifierUndefinedException();
            return Db.GetAll(_carregarPorHorario, Make, new object[] { "@SalaId ", evento.Sala.Id, "@Data_Inicio", evento.DataInicio, "@Data_Termino", evento.DataTermino });
        }

        public Evento Salvar(Evento evento)
        {
            evento.Validar();
            evento.Id = Db.Insert(_inserir, Take(evento));

            return evento;
        }

        public Evento Atualizar(Evento evento)
        {
            if (evento.Id <= 0)
                throw new IdentifierUndefinedException();
            evento.Validar();

            Db.Update(_atualizar, Take(evento));

            return evento;
        }

        public IEnumerable<Evento> CarregarPorFuncionario(long idFuncionario)
        {
            if (idFuncionario <= 0)
                throw new IdentifierUndefinedException();
            return Db.GetAll(_carregarPorFuncionario, Make, new object[] { "@IdFuncionario ", idFuncionario });
        }

        public IEnumerable<Evento> CarregarPorSala(long idSala)
        {
            if (idSala <= 0)
                throw new IdentifierUndefinedException();
            return Db.GetAll(_carregarPorSala, Make, new object[] { "@IdSala ", idSala });
        }

        private static Func<IDataReader, Evento> Make = reader =>
           new Evento
           {
               Id = Convert.ToInt64(reader["id_evento"]),
               DataInicio = Convert.ToDateTime(reader["data_inicio"]),
               DataTermino = Convert.ToDateTime(reader["data_termino"]),
               Funcionario = new Funcionario
               {
                   Id = Convert.ToInt32(reader["id_funcionario"]),
                   Cargo = Convert.ToString(reader["cargo"]),
                   Nome = Convert.ToString(reader["nome"]),
                   Ramal = Convert.ToString(reader["ramal"])
               },
               Sala = new Sala
               {
                   Id = Convert.ToInt32(reader["id_sala"]),
                   Nome = (Nome)Enum.Parse(typeof(Nome), Convert.ToString(reader["nome_sala"])),
                   NumeroLugares = Convert.ToInt32(reader["numero_lugares"])
               }
           };

        private object[] Take(Evento evento)
        {
            return new object[]
            {
                "@IdEvento", evento.Id,
                "@Data_Inicio", evento.DataInicio,
                "@Data_Termino", evento.DataTermino,
                "@Funcionario_Id", evento.Funcionario.Id,
                "@Sala_Id", evento.Sala.Id
            };
        }

        
    }
}
