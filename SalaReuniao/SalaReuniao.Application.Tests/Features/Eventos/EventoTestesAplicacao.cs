using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Application.Tests.Features.Eventos
{
    [TestFixture]
    public class EventoTestesAplicacao
    {
        private Mock<IEventoRepositorio> _mockRepositorio;
        private IEventoServico _servico;
        private Mock<Funcionario> _fakeFuncionario;
        private Mock<Sala> _fakeSala;
        private Evento _evento;

        [SetUp]
        public void Initialize()
        {
            _mockRepositorio = new Mock<IEventoRepositorio>();
            _servico = new EventoServico(_mockRepositorio.Object);
            _fakeFuncionario = new Mock<Funcionario>();
            _fakeSala = new Mock<Sala>();
        }

        [Test]
        public void Teste_EventoServico_SalvarEvento_DeveSerOk()
        {
            long idEsperado = 1;
            _evento = ObjectMother.RetorneNovoEventoOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Salvar(_evento)).Returns(ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object));

            Evento eventoSalvo = _servico.Adicionar(_evento);

            eventoSalvo.Id.Should().Be(idEsperado);
            _mockRepositorio.Verify(br => br.Salvar(_evento));
        }

        [Test]
        public void Teste_EventoServico_SalvarEventoInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoInvalidoComDataInicioMaiorQueDataTermino(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Salvar(_evento)).Returns(_evento);

            Action comparation = () => _servico.Adicionar(_evento);

            comparation.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_AtualizarEvento_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Atualizar(_evento)).Returns(_evento);

            Evento eventoAtualizado = _servico.Atualizar(_evento);

            eventoAtualizado.Id.Should().Be(_evento.Id);
            _mockRepositorio.Verify(br => br.Atualizar(_evento));
        }

        [Test]
        public void Teste_EventoServico_AtualizarEventoComIdInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _evento.Id = 0;
            _mockRepositorio.Setup(br => br.Atualizar(_evento));

            Action comparation = () => _servico.Atualizar(_evento);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_AtualizarEventoInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoInvalidoComDataInicioHorarioForaDoLimite(_fakeFuncionario.Object, _fakeSala.Object);
            _evento.Id = 1;
            _mockRepositorio.Setup(br => br.Atualizar(_evento));

            Action comparation = () => _servico.Atualizar(_evento);

            comparation.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_CarregarEvento_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Carregar(_evento.Id)).Returns(_evento);

            Evento eventoEncontrado = _servico.Carregar(_evento.Id);

            eventoEncontrado.Should().NotBeNull();
            _mockRepositorio.Verify(br => br.Carregar(_evento.Id));
        }

        [Test]
        public void Teste_EventoServico_CarregarEventoComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            _mockRepositorio.Setup(br => br.Carregar(idInvalido));

            Action comparation = () => _servico.Carregar(idInvalido);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_CarregarTodos_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.CarregarTodos()).Returns(new List<Evento> { _evento });

            var listaEventos = _servico.CarregarTodos();

            listaEventos.Should().NotBeNullOrEmpty();
            _mockRepositorio.Verify(br => br.CarregarTodos());
        }

        [Test]
        public void Teste_EventoServico_DeletarEvento_DeveSerOk()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Deletar(_evento));

            _servico.Deletar(_evento);

            _mockRepositorio.Verify(br => br.Deletar(_evento));
        }

        [Test]
        public void Teste_EventoServico_DeletarEventoComIdInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.RetorneEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _evento.Id = 0;
            _mockRepositorio.Setup(br => br.Deletar(_evento));

            Action comparation = () => _servico.Deletar(_evento);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}
