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

        public Funcionario Add(Funcionario sala)
        {
            sala.Validar();
            return _funcionarioRepositorio.Save(sala);
        }

        public void Delete(Funcionario sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            _funcionarioRepositorio.Delete(sala);
        }

        public Funcionario Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _funcionarioRepositorio.Get(id);
        }

        public IEnumerable<Funcionario> GetAll()
        {
            return _funcionarioRepositorio.GetAll();
        }

        public Funcionario Update(Funcionario sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();
            return _funcionarioRepositorio.Update(sala);
        }
    }
}
