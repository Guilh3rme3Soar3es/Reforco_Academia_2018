using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public interface IFuncionarioRepositorio
    {
        Funcionario Salvar(Funcionario funcionario);
        Funcionario Atualizar(Funcionario funcionario);
        Funcionario Carregar(long id);
        IEnumerable<Funcionario> CarregarTodos();
        void Deletar(Funcionario funcionario);
    }
}
