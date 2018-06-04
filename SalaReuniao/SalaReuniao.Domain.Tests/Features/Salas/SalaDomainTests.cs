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

namespace SalaReuniao.Domain.Tests.Features.Salas
{
    [TestFixture]
    public class SalaDomainTests
    {
        private Mock<Funcionario> _fakeFuncionario;
        private Mock<Evento> _fakeEvento1;
        private Mock<Evento> _fakeEvento2;

        [SetUp]
        public void Initialize()
        {
            _fakeFuncionario = new Mock<Funcionario>();
            _fakeEvento1 = new Mock<Evento>();
            _fakeEvento2 = new Mock<Evento>();
        }

        [Test]
        public void Teste_Sala_ValidarDeveSerOk()
        {
            Sala sala = ObjectMother.GetNovaSalaOk(_fakeEvento1.Object);
            sala.Validar();
        }

        [Test]
        public void Teste_Sala_ValidarNumeroLugaresNaoInformado_DeveSerThrowException()
        {
            Sala sala = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado(_fakeEvento1.Object);
            Action action = () => sala.Validar();
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_Sala_ValidarNumeroLugaresInvalido_DeveSerThrowException()
        {
            Sala sala = ObjectMother.GetSalaInvalidaComNumeroLugaresInvalido(_fakeEvento1.Object);
            Action action = () => sala.Validar();
            action.Should().Throw<SalaNumeroLugaresInvalido>();
        }

        [Test]
        public void Teste_Sala_ValidarEventosNoMesmoHorario_DeveSerThrowException()
        {
            _fakeEvento1.Setup(ev => ev.DataInicio).Returns(DateTime.Parse("24/03/2020 14:00:00"));
            _fakeEvento1.Setup(ev => ev.DataTermino).Returns(DateTime.Parse("24/03/2020 15:00:00"));

            _fakeEvento2.Setup(ev => ev.DataInicio).Returns(DateTime.Parse("24/03/2020 14:00:00"));
            _fakeEvento2.Setup(ev => ev.DataTermino).Returns(DateTime.Parse("24/03/2020 17:00:00"));

            Sala sala = ObjectMother.GetSalaInvalidaComEventosNoMesmoHorario(_fakeEvento1.Object, _fakeEvento2.Object);
            Action action = () => sala.Validar();
            action.Should().Throw<SalaEventosNoMesmoHorarioException>();
        }
    }
}
