using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
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

        [SetUp]
        public void Initialize()
        {
            _mockFuncionario = new Mock<Funcionario>();
        }

        [Test]
        public void Teste_Evento_Validar_DeveSerOk()
        {
            Evento evento = ObjectMother.GetNovoEventoOk(_mockFuncionario.Object);
            evento.Validar();
        }

        [Test]
        public void Teste_Evento_ValidarHorarioInicioEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioHorarioForaDoLimite(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
        }

        [Test]
        public void Teste_Evento_ValidarHorarioTerminoEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataTerminoHorarioForaDoLimite(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoHorarioForaDoLimiteException>();
        }

        [Test]
        public void Teste_Evento_ValidarDiaInicioEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioDiaNãoPermitido(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_Evento_ValidarDiaTerminoEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataTerminoDiaNãoPermitido(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataInicioMenorQueDataAtual_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioInvalida(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioInvalidaException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataTerminoMenorQueDataAtual_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataTerminoInvalida(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoInvalidaException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataInicioMaiorQueDataTermino_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComDataInicioMaiorQueDataTermino(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
        }

        [Test]
        public void Teste_Evento_ValidarListaFuncionariosNula_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComListaFuncionariosNula();
            Action action = () => evento.Validar();
            action.Should().Throw<EventoListaFuincionariosNulaOuVaziaException>();
        }

        [Test]
        public void Teste_Evento_ValidarListaFuncionariosVazia_DeveSerThrowException()
        {
            Evento evento = ObjectMother.GetEventoInvalidoComListaFuncionariosVazia();
            Action action = () => evento.Validar();
            action.Should().Throw<EventoListaFuincionariosNulaOuVaziaException>();
        }
    }
}
