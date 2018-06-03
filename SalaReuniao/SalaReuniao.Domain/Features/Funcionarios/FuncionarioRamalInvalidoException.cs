using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioRamalInvalidoException : BusinessException
    {
        public FuncionarioRamalInvalidoException() : base("Funcionario com ramal invalido.")
        {
        }
    }
}
