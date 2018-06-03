using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public interface IFuncionarioRepositorio
    {
        Funcionario Save(Funcionario funcionario);
        Funcionario Update(Funcionario funcionario);
        Funcionario Get(long id);
        IEnumerable<Funcionario> GetAll();
        void Delete(Funcionario funcionario);
    }
}
