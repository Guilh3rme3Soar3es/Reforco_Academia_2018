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
            _evento = ObjectMother.GetNovoEventoOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Save(_evento)).Returns(ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object));

            Evento eventoSalvo = _servico.Add(_evento);

            eventoSalvo.Id.Should().Be(idEsperado);
            _mockRepositorio.Verify(br => br.Save(_evento));
        }

        [Test]
        public void Teste_EventoServico_SalvarEventoInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.GetEventoInvalidoComDataInicioMaiorQueDataTermino(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Save(_evento)).Returns(_evento);

            Action comparation = () => _servico.Add(_evento);

            comparation.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_AtualizarEvento_DeveSerOk()
        {
            _evento = ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Update(_evento)).Returns(_evento);

            Evento eventoAtualizado = _servico.Update(_evento);

            eventoAtualizado.Id.Should().Be(_evento.Id);
            _mockRepositorio.Verify(br => br.Update(_evento));
        }

        [Test]
        public void Teste_EventoServico_AtualizarEventoComIdInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _evento.Id = 0;
            _mockRepositorio.Setup(br => br.Update(_evento));

            Action comparation = () => _servico.Update(_evento);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_AtualizarEventoInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.GetEventoInvalidoComDataInicioHorarioForaDoLimite(_fakeFuncionario.Object, _fakeSala.Object);
            _evento.Id = 1;
            _mockRepositorio.Setup(br => br.Update(_evento));

            Action comparation = () => _servico.Update(_evento);

            comparation.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_CarregarEvento_DeveSerOk()
        {
            _evento = ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Get(_evento.Id)).Returns(_evento);

            Evento eventoEncontrado = _servico.Get(_evento.Id);

            eventoEncontrado.Should().NotBeNull();
            _mockRepositorio.Verify(br => br.Get(_evento.Id));
        }

        [Test]
        public void Teste_EventoServico_CarregarEventoComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            _mockRepositorio.Setup(br => br.Get(idInvalido));

            Action comparation = () => _servico.Get(idInvalido);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_EventoServico_CarregarTodos_DeveSerOk()
        {
            _evento = ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.GetAll()).Returns(new List<Evento> { _evento });

            var listaEventos = _servico.GetAll();

            listaEventos.Should().NotBeNullOrEmpty();
            _mockRepositorio.Verify(br => br.GetAll());
        }

        [Test]
        public void Teste_EventoServico_DeletarEvento_DeveSerOk()
        {
            _evento = ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _mockRepositorio.Setup(br => br.Delete(_evento));

            _servico.Delete(_evento);

            _mockRepositorio.Verify(br => br.Delete(_evento));
        }

        [Test]
        public void Teste_EventoServico_DeletarEventoComIdInvalido_DeveSerThrowException()
        {
            _evento = ObjectMother.GetEventoExistenteOk(_fakeFuncionario.Object, _fakeSala.Object);
            _evento.Id = 0;
            _mockRepositorio.Setup(br => br.Delete(_evento));

            Action comparation = () => _servico.Delete(_evento);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}
