using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Common.Base;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Infra.Data.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Infra.Data.Tests.Features.Funcionarios
{
    [TestFixture]
    public class FuncionarioTestesRepositorio
    {
        private IFuncionarioRepositorio _repositorio;
        private Funcionario funcionario;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.POPULAR_BANCO();
            _repositorio = new FuncionarioRepositorio();
        }

        [Test]
        public void Teste_SalaRepositorio_SalvarSala_DeveSerOk()
        {
            long idEsperado = 3;
            funcionario = ObjectMother.GetNovaSalaOk();
            Sala salaSalva = _repositorio.Save(funcionario);
            salaSalva.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Teste_SalaRepositorio_SalvarSalaInvalida_DeveSerThrowException()
        {
            funcionario = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado();
            Action action = () => _repositorio.Save(funcionario);
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_SalaRepositorio_AtualizarSala_DeveSerOk()
        {
            funcionario = ObjectMother.GetSalaExistenteOk();
            Sala salaAtualizada = _repositorio.Update(funcionario);
            salaAtualizada.Should().Be(funcionario);
        }

        [Test]
        public void Teste_SalaRepositorio_AtualizarSalaInvalida_DeveSerThrowException()
        {
            funcionario = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado();
            funcionario.Id = 1;
            Action action = () => _repositorio.Update(funcionario);
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_SalaRepositorio_AtualizarSalaComIdInvalido_DeveSerThrowException()
        {
            funcionario = ObjectMother.GetNovaSalaOk();
            Action action = () => _repositorio.Update(funcionario);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaRepositorio_CarregarSala_DeveSerOk()
        {
            funcionario = ObjectMother.GetSalaExistenteOk();
            Sala salaEncontrada = _repositorio.Get(funcionario.Id);
            salaEncontrada.Should().NotBeNull();
            salaEncontrada.Id.Should().Be(funcionario.Id);
        }

        [Test]
        public void Teste_SalaRepositorio_CarregarSalaComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            Action action = () => _repositorio.Get(idInvalido);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaRepositorio_CarregarSalaIdNaoEncontrado_DeveSerOk()
        {
            funcionario = ObjectMother.GetSalaExistenteOk();
            funcionario.Id = 100;
            Sala salaEncontrada = _repositorio.Get(funcionario.Id);
            salaEncontrada.Should().BeNull();
        }

        [Test]
        public void Teste_SalaRepositorio_CarregarTodos_DeveSerOk()
        {
            int quantidadeEsperada = 2;
            var listaSalas = _repositorio.GetAll();
            listaSalas.Should().NotBeNullOrEmpty();
            listaSalas.Count().Should().Be(quantidadeEsperada);
        }

        [Test]
        public void Teste_SalaRepositorio_DeletarSalaComIdInvalido_DeveSerThrowException()
        {
            funcionario = ObjectMother.GetSalaExistenteOk();
            funcionario.Id = 0;
            Action action = () => _repositorio.Delete(funcionario);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaRepositorio_DeletarSala_DeveSerOk()
        {
            int quantidadeEsperada = 1;
            funcionario = ObjectMother.GetSalaExistenteOk();
            _repositorio.Delete(funcionario);

            var listaSalas = _repositorio.GetAll();
            listaSalas.Count().Should().Be(quantidadeEsperada);
        }
    }
}
