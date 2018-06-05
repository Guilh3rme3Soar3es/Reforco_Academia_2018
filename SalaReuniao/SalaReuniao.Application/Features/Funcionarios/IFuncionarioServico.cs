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
        Funcionario Adicionar(Funcionario funcionario);
        Funcionario Atualizar(Funcionario funcionario);
        Funcionario Carregar(long id);
        IEnumerable<Funcionario> CarregarTodos();
        void Deletar(Funcionario funcionario);
    }
}
