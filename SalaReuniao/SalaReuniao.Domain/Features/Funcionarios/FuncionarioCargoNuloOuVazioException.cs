using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioCargoNuloOuVazioException : BusinessException
    {
        public FuncionarioCargoNuloOuVazioException() : base("Funcionario com cargo não informado.")
        {
        }
    }
}
