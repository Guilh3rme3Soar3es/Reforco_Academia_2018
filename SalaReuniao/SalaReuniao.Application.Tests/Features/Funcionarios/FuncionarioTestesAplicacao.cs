using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Application.Features.Funcionarios;
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

namespace SalaReuniao.Application.Tests.Features.Funcionarios
{
    [TestFixture]
    public class FuncionarioTestesAplicacao
    {
        private Mock<IFuncionarioRepositorio> _mockRepositorio;
        private Mock<IEventoServico> _mockEventoServico;
        private IFuncionarioServico _servico;

        private Funcionario _funcionario;
        private Sala _sala;
        private Evento _evento;

        [SetUp]
        public void Initialize()
        {
            _mockRepositorio = new Mock<IFuncionarioRepositorio>();
            _mockEventoServico = new Mock<IEventoServico>();
            _servico = new FuncionarioServico(_mockRepositorio.Object, _mockEventoServico.Object);

            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario,_sala);
        }

        [Test]
        public void Teste_FuncionarioServico_SalvarFuncionario_DeveSerOk()
        {
            long idEsperado = 1;
            _funcionario = ObjectMother.RetorneNovoFuncionarioOk();
            _mockRepositorio.Setup(br => br.Salvar(_funcionario)).Returns(ObjectMother.RetorneFuncionarioExistenteOk());

            Funcionario funcionarioSalvo = _servico.Adicionar(_funcionario);

            funcionarioSalvo.Id.Should().Be(idEsperado);
            _mockRepositorio.Verify(br => br.Salvar(_funcionario));
        }

        [Test]
        public void Teste_FuncionarioServico_SalvarFuncionarioInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.RetorneFuncionarioComNomeNaoInformado();
            _mockRepositorio.Setup(br => br.Salvar(_funcionario)).Returns(_funcionario);

            Action comparation = () => _servico.Adicionar(_funcionario);

            comparation.Should().Throw<FuncionarioNomeNuloOuVazioException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_AtualizarFuncionario_DeveSerOk()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Atualizar(_funcionario)).Returns(_funcionario);

            Funcionario funcionarioAtualizado = _servico.Atualizar(_funcionario);

            funcionarioAtualizado.Id.Should().Be(_funcionario.Id);
            _mockRepositorio.Verify(br => br.Atualizar(_funcionario));
        }

        [Test]
        public void Teste_FuncionarioServico_AtualizarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _funcionario.Id = 0;
            _mockRepositorio.Setup(br => br.Atualizar(_funcionario));

            Action comparation = () => _servico.Atualizar(_funcionario);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_AtualizarFuncionarioInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.RetorneFuncionarioComNomeNaoInformado();
            _funcionario.Id = 1;
            _mockRepositorio.Setup(br => br.Atualizar(_funcionario));

            Action comparation = () => _servico.Atualizar(_funcionario);

            comparation.Should().Throw<FuncionarioNomeNuloOuVazioException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_CarregarFuncionario_DeveSerOk()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Carregar(_funcionario.Id)).Returns(_funcionario);

            Funcionario funcionarioEncontrado = _servico.Carregar(_funcionario.Id);

            funcionarioEncontrado.Should().NotBeNull();
            _mockRepositorio.Verify(br => br.Carregar(_funcionario.Id));
        }

        [Test]
        public void Teste_FuncionarioServico_CarregarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            _mockRepositorio.Setup(br => br.Carregar(idInvalido));

            Action comparation = () => _servico.Carregar(idInvalido);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_CarregarTodos_DeveSerOk()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.CarregarTodos()).Returns(new List<Funcionario> { _funcionario });

            var listaSalas = _servico.CarregarTodos();

            listaSalas.Should().NotBeNullOrEmpty();
            _mockRepositorio.Verify(br => br.CarregarTodos());
        }

        [Test]
        public void Teste_FuncionarioServico_DeletarFuncionario_DeveSerOk()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Deletar(_funcionario));

            _servico.Deletar(_funcionario);

            _mockRepositorio.Verify(br => br.Deletar(_funcionario));
        }

        [Test]
        public void Teste_FuncionarioServico_DeletarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _funcionario.Id = 0;
            _mockRepositorio.Setup(br => br.Deletar(_funcionario));

            Action comparation = () => _servico.Deletar(_funcionario);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_DeletarFuncionarioRelacionadoComEvento_DeveSerThrowException()
        {
            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Deletar(_funcionario));
            _mockEventoServico.Setup(es => es.CarregarPorFuncionarios(_funcionario.Id)).Returns(new List<Evento> { _evento });

            Action comparation = () => _servico.Deletar(_funcionario);

            comparation.Should().Throw<FuncionarioRelacionadoComEventoException>();
            _mockEventoServico.Verify(es => es.CarregarPorFuncionarios(_funcionario.Id));
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}

