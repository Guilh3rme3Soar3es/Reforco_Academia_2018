using NUnit.Framework;
using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Common.Base;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Infra.Data.Features.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Features.Salas;
using SalaReuniao.Domain.Features.Funcionarios;

namespace SalaReuniao.Integration.Tests.Features.Eventos
{
    [TestFixture]
    public class EventoTestesIntegracaoSistema
    {
        private IEventoRepositorio _repositorio;
        private IEventoServico _servico;

        private Sala _sala;
        private Funcionario _funcionario;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.POPULAR_BANCO();
            _repositorio = new EventoRepositorio();
            _servico = new EventoServico(_repositorio);

            _sala = ObjectMother.RetorneSalaExistenteOk();
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEvento_DeveSerOk()
        {
            Evento evento = ObjectMother.RetorneNovoEventoOk(_funcionario,_sala);
            evento = _servico.Adicionar(evento);
            evento.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComHorarioOcupado_DeveSerThrowException()
        {
            _sala.Id = 2;
            Evento evento = ObjectMother.RetorneNovoEventoOk(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoEmHorarioOcupadoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComHorarioInicioForaDoLimite_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioHorarioForaDoLimite(_funcionario,_sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComHorarioTerminoForaDoLimite_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoHorarioForaDoLimite(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataTerminoHorarioForaDoLimiteException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComInicioEmDiaNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioDiaNãoPermitido(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataInicioDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComTerminoEmDiaNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoDiaNãoPermitido(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataTerminoDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComDataInicioInvalida_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioInvalida(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataInicioInvalidaException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComDataTerminoInvalida_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoInvalida(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataTerminoInvalidaException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComDataInicioMaiorQueDataTermino_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioMaiorQueDataTermino(_funcionario, _sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComFuncionarioNaoInformado_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComFuncionarioNulo(_sala);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoFuincionarioNuloException>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEventoComSalaNaoInformada_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComSalaNula(_funcionario);
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<EventoSalaNulaException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEvento_DeveSerOk()
        {
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento = _servico.Atualizar(evento);

            Evento resultado = _servico.Carregar(evento.Id);
            resultado.Should().NotBeNull();
            resultado.DataInicio.Should().Be(evento.DataInicio);
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComHorarioOcupado_DeveSerThrowException()
        {
            int idExistente = 1;
            _sala.Id = 2;
            Evento evento = ObjectMother.RetorneNovoEventoOk(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoEmHorarioOcupadoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComHorarioInicioForaDoLimite_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioHorarioForaDoLimite(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComHorarioTerminoForaDoLimite_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoHorarioForaDoLimite(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataTerminoHorarioForaDoLimiteException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComInicioEmDiaNaoPermitido_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioDiaNãoPermitido(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataInicioDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComTerminoEmDiaNaoPermitido_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoDiaNãoPermitido(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataTerminoDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComDataInicioInvalida_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioInvalida(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataInicioInvalidaException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComDataTerminoInvalida_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoInvalida(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataTerminoInvalidaException>();
        }
        

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComDataInicioMaiorQueDataTermino_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioMaiorQueDataTermino(_funcionario, _sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComFuncionarioNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComFuncionarioNulo(_sala);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoFuincionarioNuloException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComSalaNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneEventoInvalidoComSalaNula(_funcionario);
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<EventoSalaNulaException>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizarEventoComIdInvalido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneNovoEventoOk(_funcionario, _sala);
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarEvento_DeveSerOk()
        {
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
            Evento eventoEncontrado = _servico.Carregar(evento.Id);

            eventoEncontrado.Should().NotBeNull();
            eventoEncontrado.Id.Should().Be(evento.Id);
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarEventoComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento.Id = idInvalido;
            Action action = () => _servico.Carregar(evento.Id);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarEventoComIdIdNaoEncontrado_DeveSerThrowException()
        {
            int idInvalido = 100;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento.Id = idInvalido;
            Evento eventoEncontrado = _servico.Carregar(evento.Id);

            eventoEncontrado.Should().BeNull();
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarTodos_DeveSerOk()
        {
            int queantidadeEsperada = 1;
            var listaeventos = _servico.CarregarTodos();

            listaeventos.Should().NotBeNullOrEmpty();
            listaeventos.Count().Should().Be(queantidadeEsperada);
        }

        [Test]
        public void Teste_EventoIntegracao_DeletarEvento_DeveSerOk()
        {
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            _servico.Deletar(evento);

            Evento eventoEncontrado = _servico.Carregar(evento.Id);
            eventoEncontrado.Should().BeNull();
        }

        [Test]
        public void Teste_EventoIntegracao_DeletarEventoComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento.Id = idInvalido;
            Action action = () => _servico.Deletar(evento);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarPorFuncionario_DeveSerOk()
        {
            int idRelacionado = 2;
            int quantidadeEsperada = 1;
            _funcionario.Id = idRelacionado;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);

            var listaEventos = _servico.CarregarPorFuncionarios(evento.Funcionario.Id);
            listaEventos.Should().NotBeNullOrEmpty();
            listaEventos.Count().Should().Be(quantidadeEsperada);
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarPorFuncionarioComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            _funcionario.Id = idInvalido;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento.Id = idInvalido;
            Action action = () => _servico.CarregarPorFuncionarios(evento.Funcionario.Id);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarPorSala_DeveSerOk()
        {
            int idRelacionado = 2;
            int quantidadeEsperada = 1;
            _sala.Id = idRelacionado;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);

            var listaEventos = _servico.CarregarPorSala(evento.Sala.Id);
            listaEventos.Should().NotBeNullOrEmpty();
            listaEventos.Count().Should().Be(quantidadeEsperada);
        }

        [Test]
        public void Teste_EventoIntegracao_CarregarPorSalaComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            _sala.Id = idInvalido;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento.Id = idInvalido;
            Action action = () => _servico.CarregarPorSala(evento.Sala.Id);

            action.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
