using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioRelacionadoComEventoException : BusinessException
    {
        public FuncionarioRelacionadoComEventoException() : base("Funcionario relacionado com um evento.")
        {
        }
    }
}
