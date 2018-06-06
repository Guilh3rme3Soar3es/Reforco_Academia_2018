using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Common.Base;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Domain.Features.Salas;
using SalaReuniao.Infra.Data.Features.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Infra.Data.Tests.Features.Eventos
{
    [TestFixture]
    public class EventoTestesRepositorio
    {
        private IEventoRepositorio _repositorio;
        private Funcionario _funcionario;
        private Sala _sala;
        private Evento _evento;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.POPULAR_BANCO();
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _repositorio = new EventoRepositorio();
        }

        [Test]
        public void Teste_EventoRepositorio_SalvarEvento_DeveSerOk()
        {
            long idEsperado = 2;
            _evento = ObjectMother.RetorneNovoEventoOk(_funcionario, _sala);
            Evento eventoSalvo = _repositorio.Salvar(_evento);
            eventoSalvo.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Teste_EventoRepositorio_SalvarEventoInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoInvalidoComSalaNula(_funcionario);
            Action action = () => _repositorio.Salvar(_evento);
            action.Should().Throw<EventoSalaNulaException>();
        }

        [Test]
        public void Teste_EventoRepositorio_AtualizarEvento_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
            Evento eventoAtualizado = _repositorio.Atualizar(_evento);
            eventoAtualizado.Funcionario.Id.Should().Be(_evento.Funcionario.Id);
            eventoAtualizado.Sala.Id.Should().Be(_evento.Sala.Id);
        }

        [Test]
        public void Teste_EventoRepositorio_AtualizarEventoInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoInvalidoComDataInicioInvalida(_funcionario,_sala);
            _evento.Id = 1;
            Action action = () => _repositorio.Atualizar(_evento);
            action.Should().Throw<EventoDataInicioInvalidaException>();
        }

        [Test]
        public void Teste_EventoRepositorio_AtualizarEventoComIdInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneNovoEventoOk(_funcionario,_sala);
            Action action = () => _repositorio.Atualizar(_evento);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoRepositorio_CarregarEvento_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
            Evento eventoEncontrado = _repositorio.Carregar(_evento.Id);
            eventoEncontrado.Should().NotBeNull();
            eventoEncontrado.Id.Should().Be(_evento.Id);
        }

        [Test]
        public void Teste_EventoRepositorio_CarregarEventoComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            Action action = () => _repositorio.Carregar(idInvalido);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoRepositorio_CarregarEventoIdNaoEncontrado_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
            _evento.Id = 100;
            Evento eventoEncontrado = _repositorio.Carregar(_evento.Id);
            eventoEncontrado.Should().BeNull();
        }

        [Test]
        public void Teste_EventoRepositorio_CarregarTodos_DeveSerOk()
        {
            int quantidadeEsperada = 1;
            var listaSalas = _repositorio.CarregarTodos();
            listaSalas.Should().NotBeNullOrEmpty();
            listaSalas.Count().Should().Be(quantidadeEsperada);
        }

        [Test]
        public void Teste_EventoRepositorio_DeletarEventoComIdInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
            _evento.Id = 0;
            Action action = () => _repositorio.Deletar(_evento);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoRepositorio_DeletarEvento_DeveSerOk()
        {
            int quantidadeEsperada = 0;
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
            _repositorio.Deletar(_evento);

            var listaSalas = _repositorio.CarregarTodos();
            listaSalas.Count().Should().Be(quantidadeEsperada);
        }

        [Test]
        public void Teste_EventoRepositorio_CarregarPorHorario_DeveSerOk()
        {
            int quantidadeEsperada = 1;
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            _evento.Sala.Id = 2;
            var listEventos = _repositorio.CarregarPorHorario(_evento);
            listEventos.Count().Should().Be(quantidadeEsperada);
        }
    }
}
