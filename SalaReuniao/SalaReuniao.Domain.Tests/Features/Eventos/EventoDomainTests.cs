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
            Evento evento = ObjectMother.RetorneNovoEventoOk(_mockFuncionario.Object, _fakeSala.Object);
            evento.Validar();
        }

        [Test]
        public void Teste_Evento_ValidarHorarioInicioEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioHorarioForaDoLimite(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioForaHorarioDoLimiteException>();
        }

        [Test]
        public void Teste_Evento_ValidarHorarioTerminoEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoHorarioForaDoLimite(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoHorarioForaDoLimiteException>();
        }

        [Test]
        public void Teste_Evento_ValidarDiaInicioEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioDiaNãoPermitido(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_Evento_ValidarDiaTerminoEmHorarioNaoPermitido_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoDiaNãoPermitido(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoDiaNãoPermitidoException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataInicioMenorQueDataAtual_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioInvalida(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioInvalidaException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataTerminoMenorQueDataAtual_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataTerminoInvalida(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataTerminoInvalidaException>();
        }

        [Test]
        public void Teste_Evento_ValidarDataInicioMaiorQueDataTermino_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComDataInicioMaiorQueDataTermino(_mockFuncionario.Object, _fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoDataInicioMaiorQueDataTerminoException>();
        }

        [Test]
        public void Teste_Evento_ValidarFuncionarioNulo_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComFuncionarioNulo(_fakeSala.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoFuincionarioNuloException>();
        }

        [Test]
        public void Teste_Evento_ValidarSalaNula_DeveSerThrowException()
        {
            Evento evento = ObjectMother.RetorneEventoInvalidoComSalaNula(_mockFuncionario.Object);
            Action action = () => evento.Validar();
            action.Should().Throw<EventoSalaNulaException>();
        }
    }
}
