using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Application.Features.Funcionarios;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Funcionarios;
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
        private IFuncionarioServico _servico;

        private Funcionario _funcionario;

        [SetUp]
        public void Initialize()
        {
            _mockRepositorio = new Mock<IFuncionarioRepositorio>();
            _servico = new FuncionarioServico(_mockRepositorio.Object);
        }

        [Test]
        public void Teste_FuncionarioServico_SalvarFuncionario_DeveSerOk()
        {
            long idEsperado = 1;
            _funcionario = ObjectMother.GetNovoFuncionarioOk();
            _mockRepositorio.Setup(br => br.Save(_funcionario)).Returns(ObjectMother.GetFuncionarioExistenteOk());

            Funcionario funcionarioSalvo = _servico.Add(_funcionario);

            funcionarioSalvo.Id.Should().Be(idEsperado);
            _mockRepositorio.Verify(br => br.Save(_funcionario));
        }

        [Test]
        public void Teste_FuncionarioServico_SalvarFuncionarioInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.GetFuncionarioComNomeNaoInformado();
            _mockRepositorio.Setup(br => br.Save(_funcionario)).Returns(_funcionario);

            Action comparation = () => _servico.Add(_funcionario);

            comparation.Should().Throw<FuncionarioNomeNuloOuVazioException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_AtualizarFuncionario_DeveSerOk()
        {
            _funcionario = ObjectMother.GetFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Update(_funcionario)).Returns(_funcionario);

            Funcionario funcionarioAtualizado = _servico.Update(_funcionario);

            funcionarioAtualizado.Id.Should().Be(_funcionario.Id);
            _mockRepositorio.Verify(br => br.Update(_funcionario));
        }

        [Test]
        public void Teste_FuncionarioServico_AtualizarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.GetFuncionarioExistenteOk();
            _funcionario.Id = 0;
            _mockRepositorio.Setup(br => br.Update(_funcionario));

            Action comparation = () => _servico.Update(_funcionario);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_AtualizarFuncionarioInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.GetFuncionarioComNomeNaoInformado();
            _funcionario.Id = 1;
            _mockRepositorio.Setup(br => br.Update(_funcionario));

            Action comparation = () => _servico.Update(_funcionario);

            comparation.Should().Throw<FuncionarioNomeNuloOuVazioException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_CarregarFuncionario_DeveSerOk()
        {
            _funcionario = ObjectMother.GetFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Get(_funcionario.Id)).Returns(_funcionario);

            Funcionario funcionarioEncontrado = _servico.Get(_funcionario.Id);

            funcionarioEncontrado.Should().NotBeNull();
            _mockRepositorio.Verify(br => br.Get(_funcionario.Id));
        }

        [Test]
        public void Teste_FuncionarioServico_CarregarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            _mockRepositorio.Setup(br => br.Get(idInvalido));

            Action comparation = () => _servico.Get(idInvalido);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_FuncionarioServico_CarregarTodos_DeveSerOk()
        {
            _funcionario = ObjectMother.GetFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.GetAll()).Returns(new List<Funcionario> { _funcionario });

            var listaSalas = _servico.GetAll();

            listaSalas.Should().NotBeNullOrEmpty();
            _mockRepositorio.Verify(br => br.GetAll());
        }

        [Test]
        public void Teste_FuncionarioServico_DeletarFuncionario_DeveSerOk()
        {
            _funcionario = ObjectMother.GetFuncionarioExistenteOk();
            _mockRepositorio.Setup(br => br.Delete(_funcionario));

            _servico.Delete(_funcionario);

            _mockRepositorio.Verify(br => br.Delete(_funcionario));
        }

        [Test]
        public void Teste_FuncionarioServico_DeletarFuncionarioComIdInvalido_DeveSerThrowException()
        {
            _funcionario = ObjectMother.GetFuncionarioExistenteOk();
            _funcionario.Id = 0;
            _mockRepositorio.Setup(br => br.Delete(_funcionario));

            Action comparation = () => _servico.Delete(_funcionario);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}

