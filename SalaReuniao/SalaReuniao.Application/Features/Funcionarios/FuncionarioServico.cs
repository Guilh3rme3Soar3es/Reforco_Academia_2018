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

        public FuncionarioServico(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public Funcionario Adicionar(Funcionario sala)
        {
            sala.Validar();
            return _funcionarioRepositorio.Salvar(sala);
        }

        public void Deletar(Funcionario sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            _funcionarioRepositorio.Deletar(sala);
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

        public Funcionario Atualizar(Funcionario sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();
            return _funcionarioRepositorio.Atualizar(sala);
        }
    }
}
