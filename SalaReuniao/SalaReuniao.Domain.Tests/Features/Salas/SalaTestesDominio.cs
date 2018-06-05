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
    public class SalaTestesDominio
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
            Sala sala = ObjectMother.RetorneNovaSalaOk();
            sala.Validar();
        }

        [Test]
        public void Teste_Sala_ValidarNumeroLugaresNaoInformado_DeveSerThrowException()
        {
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresNaoInformado();
            Action action = () => sala.Validar();
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_Sala_ValidarNumeroLugaresInvalido_DeveSerThrowException()
        {
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresInvalido();
            Action action = () => sala.Validar();
            action.Should().Throw<SalaNumeroLugaresInvalido>();
        }
    }
}
