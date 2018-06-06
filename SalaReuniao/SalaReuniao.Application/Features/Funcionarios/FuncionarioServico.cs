using SalaReuniao.Application.Features.Eventos;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Application.Features.Funcionarios
{
    public class FuncionarioServico : IFuncionarioServico
    {
        private IFuncionarioRepositorio _funcionarioRepositorio;
        private IEventoServico _eventoservico;

        public FuncionarioServico(IFuncionarioRepositorio funcionarioRepositorio, IEventoServico eventoservico)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _eventoservico = eventoservico;
        }

        public Funcionario Adicionar(Funcionario funcionario)
        {
            funcionario.Validar();
            return _funcionarioRepositorio.Salvar(funcionario);
        }

        public void Deletar(Funcionario funcionario)
        {
            if (funcionario.Id <= 0)
                throw new IdentifierUndefinedException();
            if (_eventoservico.CarregarPorFuncionarios(funcionario.Id).Count() > 0)
                throw new FuncionarioRelacionadoComEventoException();
            _funcionarioRepositorio.Deletar(funcionario);
        }

        public Funcionario Carregar(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _funcionarioRepositorio.Carregar(id);
        }

        public IEnumerable<Funcionario> CarregarTodos()
        {
            return _funcionarioRepositorio.CarregarTodos();
        }

        public Funcionario Atualizar(Funcionario funcionario)
        {
            if (funcionario.Id <= 0)
                throw new IdentifierUndefinedException();
            funcionario.Validar();
            return _funcionarioRepositorio.Atualizar(funcionario);
        }
    }
}
