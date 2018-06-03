using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioRamalNuloOuVazioException : BusinessException
    {
        public FuncionarioRamalNuloOuVazioException() : base("Funcionario com ramal não informado.")
        {
        }
    }
}
