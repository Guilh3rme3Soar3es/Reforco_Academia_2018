using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Common.Base;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Salas;
using SalaReuniao.Infra.Data.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Infra.Data.Tests.Features.Salas
{
    [TestFixture]
    public class SalaTestesRepositorio
    {
        private ISalaRepositorio _repositorio;
        private Sala _sala;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.POPULAR_BANCO();
            _repositorio = new SalaRepositorio();
        }

        [Test]
        public void Teste_SalaRepositorio_SalvarSala_DeveSerOk()
        {
            long idEsperado = 3;
            _sala = ObjectMother.GetNovaSalaOk();
            Sala salaSalva = _repositorio.Save(_sala);
            salaSalva.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Teste_SalaRepositorio_SalvarSalaInvalida_DeveSerThrowException()
        {
            _sala = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado();
            Action action = () => _repositorio.Save(_sala);
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_SalaRepositorio_AtualizarSala_DeveSerOk()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            Sala salaAtualizada = _repositorio.Update(_sala);
            salaAtualizada.Should().Be(_sala);
        }

        [Test]
        public void Teste_SalaRepositorio_AtualizarSalaInvalida_DeveSerThrowException()
        {
            _sala = ObjectMother.GetSalaInvalidaComNumeroLugaresNaoInformado();
            _sala.Id = 1;
            Action action = () => _repositorio.Update(_sala);
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_SalaRepositorio_AtualizarSalaComIdInvalido_DeveSerThrowException()
        {
            _sala = ObjectMother.GetNovaSalaOk();
            Action action = () => _repositorio.Update(_sala);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaRepositorio_CarregarSala_DeveSerOk()
        {
            _sala = ObjectMother.GetSalaExistenteOk();
            Sala salaEncontrada = _repositorio.Get(_sala.Id);
            salaEncontrada.Should().NotBeNull();
            salaEncontrada.Id.Should().Be(_sala.Id);
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
            _sala = ObjectMother.GetSalaExistenteOk();
            _sala.Id = 100;
            Sala salaEncontrada = _repositorio.Get(_sala.Id);
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
            _sala = ObjectMother.GetSalaExistenteOk();
            _sala.Id = 0;
            Action action = () => _repositorio.Delete(_sala);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaRepositorio_DeletarSala_DeveSerOk()
        {
            int quantidadeEsperada = 1;
            _sala = ObjectMother.GetSalaExistenteOk();
            _repositorio.Delete(_sala);

            var listaSalas = _repositorio.GetAll();
            listaSalas.Count().Should().Be(quantidadeEsperada);
        }
    }
}
