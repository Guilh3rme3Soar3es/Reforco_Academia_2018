using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Tests.Features.Funcionarios
{
    [TestFixture]
    public class FuncionarioDomainTests
    {
        [Test]
        public void Teste_Funcionario_Validar_DeveSerOk()
        {
            Funcionario funcionario = ObjectMother.GetNovoFuncionarioOk();
            funcionario.Validar();
        }

        [Test]
        public void Teste_Funcionario_ValidarNomeNaoInformado_DeverSerThrowException()
        {
            Funcionario funcionario = ObjectMother.GetFuncionarioComNomeNaoInformado();
            Action action = () => funcionario.Validar();
            action.Should().Throw<FuncionarioNomeNuloOuVazioException>();
        }

        [Test]
        public void Teste_Funcionario_ValidarNomeMuitoLongo_DeverSerThrowException()
        {
            Funcionario funcionario = ObjectMother.GetFuncionarioComNomeLongo();
            Action action = () => funcionario.Validar();
            action.Should().Throw<FuncionarioEstouroDeLarguraDoNomeException>();
        }

        [Test]
        public void Teste_Funcionario_ValidarCargoNaoInformado_DeverSerThrowException()
        {
            Funcionario funcionario = ObjectMother.GetFuncionarioComCargoNaoInformado();
            Action action = () => funcionario.Validar();
            action.Should().Throw<FuncionarioCargoNuloOuVazioException>();
        }

        [Test]
        public void Teste_Funcionario_ValidarCargoMuitoLongo_DeverSerThrowException()
        {
            Funcionario funcionario = ObjectMother.GetFuncionarioComCargoLongo();
            Action action = () => funcionario.Validar();
            action.Should().Throw<FuncionarioEstouroDeLarguraDeCargoException>();
        }

        [Test]
        public void Teste_Funcionario_ValidarRamalNaoInformado_DeverSerThrowException()
        {
            Funcionario funcionario = ObjectMother.GetFuncionarioComRamalNaoInformado();
            Action action = () => funcionario.Validar();
            action.Should().Throw<FuncionarioRamalNuloOuVazioException>();
        }

        [Test]
        public void Teste_Funcionario_ValidarRamalInvalido_DeverSerThrowException()
        {
            Funcionario funcionario = ObjectMother.GetFuncionarioComRamalInvalido();
            Action action = () => funcionario.Validar();
            action.Should().Throw<FuncionarioRamalInvalidoException>();
        }
    }
}
