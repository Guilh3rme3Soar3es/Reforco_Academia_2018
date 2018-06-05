using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Common.Base;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
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
        public void Teste_FuncionarioRepositorio_SalvarFuncionario_DeveSerOk()
        {
            long idEsperado = 3;
            funcionario = ObjectMother.RetorneNovoFuncionarioOk();
            Funcionario funcionarioSalvo = _repositorio.Salvar(funcionario);
            funcionarioSalvo.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Teste_FuncionarioRepositorio_SalvarFuncionarioInvalida_DeveSerThrowException()
        {
            funcionario = ObjectMother.RetorneFuncionarioComNomeNaoInformado();
            Action action = () => _repositorio.Salvar(funcionario);
            action.Should().Throw<FuncionarioNomeNuloOuVazioException>();
        }

        [Test]
        public void Teste_FuncionarioRepositorio_AtualizarFuncionario_DeveSerOk()
        {
            funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            Funcionario funcionarioAtualizado = _repositorio.Atualizar(funcionario);
            funcionarioAtualizado.Should().Be(funcionario);
        }

        [Test]
        public void Teste_FuncionarioRepositorio_AtualizarFuncionarioInvalida_DeveSerThrowException()
        {
            funcionario = ObjectMother.RetorneFuncionarioComNomeNaoInformado();
            funcionario.Id = 1;
            Action action = () => _repositorio.Atualizar(funcionario);
            action.Should().Throw<FuncionarioNomeNuloOuVazioException>();
        }

        [Test]
        public void Teste_FuncionarioRepositorio_AtualizarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            funcionario = ObjectMother.RetorneNovoFuncionarioOk();
            Action action = () => _repositorio.Atualizar(funcionario);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_FuncionarioRepositorio_CarregarFuncionario_DeveSerOk()
        {
            funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            Funcionario funcionarioEncontrado = _repositorio.Carregar(funcionario.Id);
            funcionarioEncontrado.Should().NotBeNull();
            funcionarioEncontrado.Id.Should().Be(funcionario.Id);
        }

        [Test]
        public void Teste_FuncionarioRepositorio_CarregarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            Action action = () => _repositorio.Carregar(idInvalido);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_FuncionarioRepositorio_CarregarFuncionarioIdNaoEncontrado_DeveSerOk()
        {
            funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario.Id = 100;
            Funcionario funcionarioEncontrado = _repositorio.Carregar(funcionario.Id);
            funcionarioEncontrado.Should().BeNull();
        }

        [Test]
        public void Teste_FuncionarioRepositorio_CarregarTodos_DeveSerOk()
        {
            int quantidadeEsperada = 2;
            var listaSalas = _repositorio.CarregarTodos();
            listaSalas.Should().NotBeNullOrEmpty();
            listaSalas.Count().Should().Be(quantidadeEsperada);
        }

        [Test]
        public void Teste_FuncionarioRepositorio_DeletarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario.Id = 0;
            Action action = () => _repositorio.Deletar(funcionario);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_FuncionarioRepositorio_DeletarFuncionario_DeveSerOk()
        {
            int quantidadeEsperada = 1;
            funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _repositorio.Deletar(funcionario);

            var listaFuncionarios = _repositorio.CarregarTodos();
            listaFuncionarios.Count().Should().Be(quantidadeEsperada);
        }
    }
}
