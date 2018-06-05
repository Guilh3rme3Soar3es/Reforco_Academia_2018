using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Tests.Features.Eventos
{
    [TestFixture]
    public class EventoDomainTests
    {
        private Mock<Funcionario> _mockFuncionario;
        private Mock<Sala> _fakeSala;

        [SetUp]
        public void Initialize()
        {
            _mockFuncionario = new Mock<Funcionario>();
            _fakeSala = new Mock<Sala>();
        }

        [Test]
        public void Teste_Evento_Validar_DeveSerOk()
        {
            Evento evento = ObjectMother.GetNovoEventoOk(_mockFuncionario.Object, _fakeSala.Object);
            evento.Validar();
        }

        [Test]
        public void Teste_Evento_ValidarHorarioInicioEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioHorarioForaDoLimite(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
        }

        [Test]
        public void Teste_Evento_ValidarHorarioTerminoEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataTerminoHorarioForaDoLimite(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoHorarioForaDoLimiteException>();
        }

        [Test]
        public void Teste_Evento_ValidarDiaInicioEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioDiaNãoPermitido(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_Evento_ValidarDiaTerminoEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataTerminoDiaNãoPermitido(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataInicioMenorQueDataAtual_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioInvalida(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioInvalidaException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataTerminoMenorQueDataAtual_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataTerminoInvalida(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoInvalidaException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataInicioMaiorQueDataTermino_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioMaiorQueDataTermino(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
        }

        [Test]
        public void Teste_Evento_ValidarFuncionarioNulo_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComFuncionarioNulo(_fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoFuincionarioNuloException>();
        }

        [Test]
        public void Teste_Evento_ValidarSalaNula_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComSalaNula(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoSalaNulaException>();
        }
    }
}
