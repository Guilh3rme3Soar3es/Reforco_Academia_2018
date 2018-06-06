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
            _funcionario = ObjectMother.RetorneNovoFuncionarioOk();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvarEvento_DeveSerOk()
        {
            Evento evento = ObjectMother.RetorneNovoEventoOk(_funcionario,_sala);
            evento = _servico.Adicionar(evento);
            evento.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Teste_EventoIntegracao_SalvareventoComNumeroLugaresNaoInformado_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneeventoInvalidaComNumeroLugaresNaoInformado();
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<eventoNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_EventoIntegracao_SalvareventoComNumeroLugaresInvalido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneeventoInvalidaComNumeroLugaresInvalido();
            Action action = () => _servico.Adicionar(evento);
            action.Should().Throw<eventoNumeroLugaresInvalido>();
        }

        [Test]
        public void Teste_EventoIntegracao_Atualizarevento_DeveSerOk()
        {
            Evento evento = ObjectMother.RetorneeventoExistenteOk();
            evento = _servico.Atualizar(evento);

            Evento resultado = _servico.Carregar(evento.Id);
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(evento.Nome);
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizareventoComNumeroLugaresNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneeventoInvalidaComNumeroLugaresNaoInformado();
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<eventoNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_EventoIntegracao_AtualizareventoComNumeroLugaresInvalido_DeveSerThrowException()
        {
            int idExistente = 1;
            Evento evento = ObjectMother.RetorneeventoInvalidaComNumeroLugaresInvalido();
            evento.Id = idExistente;
            Action action = () => _servico.Atualizar(evento);

            action.Should().Throw<eventoNumeroLugaresInvalido>();
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
            int queantidadeEsperada = 2;
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
        public void Teste_EventoIntegracao_DeletarevEntoComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Evento evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
            evento.Id = idInvalido;
            Action action = () => _servico.Deletar(evento);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_EventoIntegracao_DeletarevEntoRelacionadaComEvento_DeveSerThrowException()
        {
            int idRelacionado = 2;
            Evento evento = ObjectMother.RetorneeventoExistenteOk();
            evento.Id = idRelacionado;
            Action action = () => _servico.Deletar(evento);

            action.Should().Throw<eventoRelacionadaComEventoException>();
        }
    }
}
