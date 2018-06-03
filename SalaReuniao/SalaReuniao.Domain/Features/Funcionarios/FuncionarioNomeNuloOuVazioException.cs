using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioNomeNuloOuVazioException : BusinessException
    {
        public FuncionarioNomeNuloOuVazioException() : base("Funcionario com nome não informado.")
        {
        }
    }
}
