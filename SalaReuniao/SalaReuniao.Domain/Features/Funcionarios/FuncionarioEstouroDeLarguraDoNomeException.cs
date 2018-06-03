using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioEstouroDeLarguraDoNomeException : BusinessException
    {
        public FuncionarioEstouroDeLarguraDoNomeException() : base("Fncionario com nome maior que 100 caracteres.")
        {
        }
    }
}
