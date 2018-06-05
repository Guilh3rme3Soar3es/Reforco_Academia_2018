using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaReuniao.Application.Features.Salas;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
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
        private ISalaServico _servico;

        private Sala _sala;

        [SetUp]
        public void Initialize()
        {
            _mockRepositorio = new Mock<ISalaRepositorio>();
            _servico = new SalaServico(_mockRepositorio.Object);
        }

        [Test]
        public void Teste_SalaServico_SalvarSala_DeveSerOk()
        {
            long idEsperado = 1;
            _sala = ObjectMother.GetNovaSalaOk();
            _mockRepositorio.Setup(br => br.Save(_sala)).Returns(ObjectMother.GetSalaExistenteOk());

            Sala salaSalva = _servico.Add(_sala);

            salaSalva.Id.Should().Be(idEsperado);
            _mockRepositorio.Verify(br => br.Save(_sala));
        }

        [Test]
        public void Teste_SalaServico_SalvarSalaInvalida_DeveSerThrowException()
        {
            _sala = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado();
            _mockRepositorio.Setup(br => br.Save(_sala)).Returns(_sala);

            Action comparation = () => _servico.Add(_sala);

            comparation.Should().Throw<SalaNumeroLugaresNaoInformado>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_AtualizarSala_DeveSerOk()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Update(_sala)).Returns(_sala);

            Sala salaAtualizada = _servico.Update(_sala);

            salaAtualizada.Id.Should().Be(_sala.Id);
            _mockRepositorio.Verify(br => br.Update(_sala));
        }

        [Test]
        public void Teste_SalaServico_AtualizarSalaComIdInvalido_DeveSerThrowException()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            _sala.Id = 0;
            _mockRepositorio.Setup(br => br.Update(_sala));

            Action comparation = () => _servico.Update(_sala);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_AtualizarSalaInvalida_DeveSerThrowException()
        {
            _sala = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado();
            _sala.Id = 1;
            _mockRepositorio.Setup(br => br.Update(_sala));

            Action comparation = () => _servico.Update(_sala);

            comparation.Should().Throw<SalaNumeroLugaresNaoInformado>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_CarregarSala_DeveSerOk()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Get(_sala.Id)).Returns(_sala);

            Sala SalaEncontrada = _servico.Get(_sala.Id);

            SalaEncontrada.Should().NotBeNull();
            _mockRepositorio.Verify(br => br.Get(_sala.Id));
        }

        [Test]
        public void Teste_SalaServico_CarregarSalaComIdInvalido_DeveSerThrowException()
        {
            long idInvalido = 0;
            _mockRepositorio.Setup(br => br.Get(idInvalido));

            Action comparation = () => _servico.Get(idInvalido);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }

        [Test]
        public void Teste_SalaServico_CarregarTodos_DeveSerOk()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            _mockRepositorio.Setup(br => br.GetAll()).Returns(new List<Sala> { _sala });

            var listaSalas = _servico.GetAll();

            listaSalas.Should().NotBeNullOrEmpty();
            _mockRepositorio.Verify(br => br.GetAll());
        }

        [Test]
        public void Teste_SalaServico_DeletarSala_DeveSerOk()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            _mockRepositorio.Setup(br => br.Delete(_sala));

            _servico.Delete(_sala);

            _mockRepositorio.Verify(br => br.Delete(_sala));
        }

        [Test]
        public void Teste_SalaServico_DeletarSalaComIdInvalido_DeveSerThrowException()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            _sala.Id = 0;
            _mockRepositorio.Setup(br => br.Delete(_sala));

            Action comparation = () => _servico.Delete(_sala);

            comparation.Should().Throw<IdentifierUndefinedException>();
            _mockRepositorio.VerifyNoOtherCalls();
        }
    }
}
