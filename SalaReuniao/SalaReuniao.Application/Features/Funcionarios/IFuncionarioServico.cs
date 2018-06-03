using SalaReuniao.Domain.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Application.Features.Funcionarios
{
    public interface IFuncionarioServico
    {
        Funcionario Add(Funcionario funcionario);
        Funcionario Update(Funcionario funcionario);
        Funcionario Get(long id);
        IEnumerable<Funcionario> GetAll();
        void Delete(Funcionario funcionario);
    }
}
