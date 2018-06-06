using FluentAssertions;
using NUnit.Framework;
using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Application.Features.Salas;
using SalaReuniao.Common.Base;
using SalaReuniao.Common.Common.Features.ObjectMothers;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Salas;
using SalaReuniao.Infra.Data.Features.Eventos;
using SalaReuniao.Infra.Data.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Integration.Tests.Features.Salas
{
    [TestFixture]
    public class SalaTestesIntegracaoSistema
    {
        private ISalaRepositorio _salaRepositorio;
        private IEventoRepositorio _eventoRepositorio;
        private IEventoServico _eventoServico;
        private ISalaServico _salaServico;


        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.POPULAR_BANCO();
            _salaRepositorio = new SalaRepositorio();
            _eventoRepositorio = new EventoRepositorio();
            _eventoServico = new EventoServico(_eventoRepositorio);
            _salaServico = new SalaServico(_salaRepositorio, _eventoServico);
        }

        [Test]
        public void Teste_SalaIntegracao_SalvarSala_DeveSerOk()
        {
            Sala sala = ObjectMother.RetorneNovaSalaOk();
            sala = _salaServico.Adicionar(sala);
            sala.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Teste_SalaIntegracao_SalvarSalaComNumeroLugaresNaoInformado_DeveSerThrowException()
        {
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresNaoInformado();
            Action action = () => _salaServico.Adicionar(sala);
            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_SalaIntegracao_SalvarSalaComNumeroLugaresInvalido_DeveSerThrowException()
        {
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresInvalido();
            Action action = () => _salaServico.Adicionar(sala);
            action.Should().Throw<SalaNumeroLugaresInvalido>();
        }

        [Test]
        public void Teste_SalaIntegracao_AtualizarSala_DeveSerOk()
        {
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            sala = _salaServico.Atualizar(sala);

            Sala resultado = _salaServico.Carregar(sala.Id);
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(sala.Nome);
        }

        [Test]
        public void Teste_SalaIntegracao_AtualizarSalaComNumeroLugaresNaoInformado_DeveSerThrowException()
        {
            int idExistente = 1;
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresNaoInformado();
            sala.Id = idExistente;
            Action action = () => _salaServico.Atualizar(sala);

            action.Should().Throw<SalaNumeroLugaresNaoInformado>();
        }

        [Test]
        public void Teste_SalaIntegracao_AtualizarSalaComNumeroLugaresInvalido_DeveSerThrowException()
        {
            int idExistente = 1;
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresInvalido();
            sala.Id = idExistente;
            Action action = () => _salaServico.Atualizar(sala);

            action.Should().Throw<SalaNumeroLugaresInvalido>();
        }

        [Test]
        public void Teste_SalaIntegracao_AtualizarSalaComIdInvalido_DeveSerThrowException()
        {
            Sala sala = ObjectMother.RetorneSalaInvalidaComNumeroLugaresInvalido();
            Action action = () => _salaServico.Atualizar(sala);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaIntegracao_CarregarSala_DeveSerOk()
        {
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            Sala salaEncontrada = _salaServico.Carregar(sala.Id);

            salaEncontrada.Should().NotBeNull();
            salaEncontrada.Id.Should().Be(sala.Id);
        }

        [Test]
        public void Teste_SalaIntegracao_CarregarSalaComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            sala.Id = idInvalido;
            Action action = () => _salaServico.Carregar(sala.Id);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaIntegracao_CarregarSalaComIdIdNaoEncontrado_DeveSerThrowException()
        {
            int idInvalido = 100;
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            sala.Id = idInvalido;
            Sala salaEncontrada = _salaServico.Carregar(sala.Id);

            salaEncontrada.Should().BeNull();
        }

        [Test]
        public void Teste_SalaIntegracao_CarregarTodos_DeveSerOk()
        {
            int queantidadeEsperada = 2;
            var listaSalas = _salaServico.CarregarTodos();

            listaSalas.Should().NotBeNullOrEmpty();
            listaSalas.Count().Should().Be(queantidadeEsperada);
        }

        [Test]
        public void Teste_SalaIntegracao_DeletarSala_DeveSerOk()
        {
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            _salaServico.Deletar(sala);

            Sala salaEncontrada = _salaServico.Carregar(sala.Id);
            salaEncontrada.Should().BeNull();
        }

        [Test]
        public void Teste_SalaIntegracao_DeletarSalaComIdInvalido_DeveSerThrowException()
        {
            int idInvalido = 0;
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            sala.Id = idInvalido;
            Action action = () => _salaServico.Deletar(sala);

            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Teste_SalaIntegracao_DeletarSalaRelacionadaComEvento_DeveSerThrowException()
        {
            int idRelacionado = 2;
            Sala sala = ObjectMother.RetorneSalaExistenteOk();
            sala.Id = idRelacionado;
            Action action = () => _salaServico.Deletar(sala);

            action.Should().Throw<SalaRelacionadaComEventoException>();
        }
    }
}
