using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Application.Features.Salas;
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

namespace SalaReuniao.Application.Tests.Features.Salas
{
    [TestFixture]
    public class SalaTestesAplicacao
    {
        private Mock<ISalaRepositorio> _mockRepositorio;
        private Mock<IEventoServico> _mockEventoServico;
        private ISalaServico _servico;

        private Funcionario _funcionario;
        private Sala _sala;
        private Evento _evento;

        [SetUp]
        public void Initialize()
        {
            _mockEventoServico = new Mock<IEventoServico>();
            _mockRepositorio = new Mock<ISalaRepositorio>();
            _servico = new SalaServico(_mockRepositorio.Object, _mockEventoServico.Object);

            _funcionario = ObjectMother.RetorneFuncionarioExistenteOk();
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _evento = ObjectMother.RetorneEventoExistenteOk(_funcionario, _sala);
        }

        [Test]
        public void Teste_SalaServico_SalvarSala_DeveSerOk()
        {
            long idEsperado = 1;
            _sala = ObjectMother.RetorneNovaSalaOk();
            _mockRepositorio.Setup(br => br.Salvar(_sala)).Returns(ObjectMother.RetorneSalaExistenteOk());

            Sala salaSalva = _servico.Adicionar(_sala);

            salaSalva.Id.Should().Be(idEsperado);
            _mockRepositorio.Verify(br => br.Salvar(_sala));
        }

        [Test]
        public void Teste_SalaServico_SalvarSalaInvalida_DeveSerThrowException()
        {
            _sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresNaoInformado();
            _mockRepositorio.Setup(br => br.Salvar(_sala)).Returns(_sala);

            Action comparation = () => _servico.Adicionar(_sala);

            comparation.Should().Throw<SalaNumeroLugaresNaoInformado>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_AtualizarSala_DeveSerOk()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Atualizar(_sala)).Returns(_sala);

            Sala salaAtualizada = _servico.Atualizar(_sala);

            salaAtualizada.Id.Should().Be(_sala.Id);
            _mockRepositorio.Verify(br => br.Atualizar(_sala));
        }

        [Test]
        public void Teste_SalaServico_AtualizarSalaComIdInvalido_DeveSerThrowException()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _sala.Id = 0;
            _mockRepositorio.Setup(br => br.Atualizar(_sala));

            Action comparation = () => _servico.Atualizar(_sala);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_AtualizarSalaInvalida_DeveSerThrowException()
        {
            _sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresNaoInformado();
            _sala.Id = 1;
            _mockRepositorio.Setup(br => br.Atualizar(_sala));

            Action comparation = () => _servico.Atualizar(_sala);

            comparation.Should().Throw<SalaNumeroLugaresNaoInformado>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_CarregarSala_DeveSerOk()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Carregar(_sala.Id)).Returns(_sala);

            Sala SalaEncontrada = _servico.Carregar(_sala.Id);

            SalaEncontrada.Should().NotBeNull();
            _mockRepositorio.Verify(br => br.Carregar(_sala.Id));
        }

        [Test]
        public void Teste_SalaServico_CarregarSalaComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            _mockRepositorio.Setup(br => br.Carregar(idInvalido));

            Action comparation = () => _servico.Carregar(idInvalido);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_CarregarTodos_DeveSerOk()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _mockRepositorio.Setup(br => br.CarregarTodos()).Returns(new List<Sala> { _sala });

            var listaSalas = _servico.CarregarTodos();

            listaSalas.Should().NotBeNullOrEmpty();
            _mockRepositorio.Verify(br => br.CarregarTodos());
        }

        [Test]
        public void Teste_SalaServico_DeletarSala_DeveSerOk()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Deletar(_sala));

            _servico.Deletar(_sala);

            _mockRepositorio.Verify(br => br.Deletar(_sala));
        }

        [Test]
        public void Teste_SalaServico_DeletarSalaComIdInvalido_DeveSerThrowException()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _sala.Id = 0;
            _mockRepositorio.Setup(br => br.Deletar(_sala));

            Action comparation = () => _servico.Deletar(_sala);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_DeletarSalaRelacionadaComEvento_DeveSerThrowException()
        {
            _sala = ObjectMother.RetorneSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Deletar(_sala));
            _mockEventoServico.Setup(es => es.CarregarPorSala(_sala.Id)).Returns(new List<Evento> { _evento });

            Action comparation = () => _servico.Deletar(_sala);

            comparation.Should().Throw<SalaRelacionadaComEventoException>();
            _mockEventoServico.Verify(es => es.CarregarPorSala(_sala.Id));
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}
