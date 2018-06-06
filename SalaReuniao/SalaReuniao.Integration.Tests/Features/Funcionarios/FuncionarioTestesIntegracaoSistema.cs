using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Application.Features.Funcionarios;
using SalaReuniao.Common.Base;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Infra.Data.Features.Eventos;
using SalaReuniao.Infra.Data.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Integration.Tests.Features.Funcionarios
{
    [TestFixture]
    public class FuncionarioTestesIntegracaoSistema
    {
        private IFuncionarioRepositorio _funcionarioRepositorio;
        private IEventoRepositorio _eventoRepositorio;
        private IEventoServico _eventoServico;
        private IFuncionarioServico _funcionarioServico;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.POPULAR_BANCO();
            _funcionarioRepositorio = new FuncionarioRepositorio();
            _eventoRepositorio = new EventoRepositorio();
            _eventoServico = new EventoServico(_eventoRepositorio);
            _funcionarioServico = new FuncionarioServico(_funcionarioRepositorio, _eventoServico);
        }

        [Test]
        public void Teste_FuncionarioIntegracao_SalvarFuncionario_DeveSerOk()
        {
            Funcionario funcionario = ObjectMother.RetorneNovoFuncionarioOk();
            funcionario = _funcionarioServico.Adicionar(funcionario);
            funcionario.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Teste_FuncionarioIntegracao_SalvarFuncionarioComNomeMuitoLongo_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComNomeLongo();
            Action action = () => _funcionarioServico.Adicionar(funcionario);
            action.Should().Throw<FuncionarioEstouroDeLarguraDoNomeException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_SalvarFuncionarioComCargoMuitoLongo_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComCargoLongo();
            Action action = () => _funcionarioServico.Adicionar(funcionario);
            action.Should().Throw<FuncionarioEstouroDeLarguraDeCargoException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_SalvarFuncionarioComRamalInvalido_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComRamalInvalido();
            Action action = () => _funcionarioServico.Adicionar(funcionario);
            action.Should().Throw<FuncionarioRamalInvalidoException>();
        }

        [Test]
        public void Teste_SalaIntegracao_SalvarFuncionarioComNomeNaoInformado_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComNomeNaoInformado();
            Action action = () => _funcionarioServico.Adicionar(funcionario);
            action.Should().Throw<FuncionarioNomeNuloOuVazioException>();
        }

        [Test]
        public void Teste_SalaIntegracao_SalvarFuncionarioComCargoNaoInformado_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComCargoNaoInformado();
            Action action = () => _funcionarioServico.Adicionar(funcionario);
            action.Should().Throw<FuncionarioCargoNuloOuVazioException>();
        }

        [Test]
        public void Teste_SalaIntegracao_SalvarFuncionarioComRamalNaoInformado_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComRamalNaoInformado();
            Action action = () => _funcionarioServico.Adicionar(funcionario);
            action.Should().Throw<FuncionarioRamalNuloOuVazioException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionario_DeveSerOk()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario = _funcionarioServico.Atualizar(funcionario);

            Funcionario resultado = _funcionarioServico.Carregar(funcionario.Id);
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(funcionario.Nome);
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComNomeMuitoLongo_DeveSerThrowException()
        {
            int idExistente = 1;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComNomeLongo();
            funcionario.Id = idExistente;
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<FuncionarioEstouroDeLarguraDoNomeException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComCargoMuitoLongo_DeveSerThrowException()
        {
            int idExistente = 1;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComCargoLongo();
            funcionario.Id = idExistente;
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<FuncionarioEstouroDeLarguraDeCargoException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComRamalInvalido_DeveSerThrowException()
        {
            int idExistente = 1;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComRamalInvalido();
            funcionario.Id = idExistente;
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<FuncionarioRamalInvalidoException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComNomeNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComNomeNaoInformado();
            funcionario.Id = idExistente;
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<FuncionarioNomeNuloOuVazioException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComCargoNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComCargoNaoInformado();
            funcionario.Id = idExistente;
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<FuncionarioCargoNuloOuVazioException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComCargoRamalNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioComRamalNaoInformado();
            funcionario.Id = idExistente;
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<FuncionarioRamalNuloOuVazioException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_AtualizarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            Funcionario funcionario = ObjectMother.RetorneNovoFuncionarioOk();
            Action action = () => _funcionarioServico.Atualizar(funcionario);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_CarregarFuncionario_DeveSerOk()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            Funcionario funcionarioEncontrado = _funcionarioServico.Carregar(funcionario.Id);

            funcionarioEncontrado.Should().NotBeNull();
            funcionarioEncontrado.Id.Should().Be(funcionario.Id);
        }

        [Test]
        public void Teste_FuncionarioIntegracao_CarregarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario.Id = idInvalido;
            Action action = () => _funcionarioServico.Carregar(funcionario.Id);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_CarregarFuncionarioComIdIdNaoEncontrado_DeveSerThrowException()
        {
            int idInvalido = 100;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario.Id = idInvalido;
            Funcionario funcionarioEncontrado = _funcionarioServico.Carregar(funcionario.Id);

            funcionarioEncontrado.Should().BeNull();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_CarregarTodos_DeveSerOk()
        {
            int queantidadeEsperada = 2;
            var listaFuncionarios = _funcionarioServico.CarregarTodos();

            listaFuncionarios.Should().NotBeNullOrEmpty();
            listaFuncionarios.Count().Should().Be(queantidadeEsperada);
        }

        [Test]
        public void Teste_FuncionarioIntegracao_DeletarFuncionario_DeveSerOk()
        {
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _funcionarioServico.Deletar(funcionario);

            Funcionario salaEncontrada = _funcionarioServico.Carregar(funcionario.Id);
            salaEncontrada.Should().BeNull();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_DeletarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario.Id = idInvalido;
            Action action = () => _funcionarioServico.Deletar(funcionario);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_FuncionarioIntegracao_DeletarFuncionarioRelacionadaComEvento_DeveSerThrowException()
        {
            int idRelacionado = 2;
            Funcionario funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            funcionario.Id = idRelacionado;
            Action action = () => _funcionarioServico.Deletar(funcionario);

            action.Should().Throw<FuncionarioRelacionadoComEventoException>();
        }
    }
}
