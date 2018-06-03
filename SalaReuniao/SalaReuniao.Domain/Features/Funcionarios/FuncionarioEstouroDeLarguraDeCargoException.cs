using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class FuncionarioEstouroDeLarguraDeCargoException : BusinessException
    {
        public FuncionarioEstouroDeLarguraDeCargoException() : base("Funcionario com cargo maior que 50 caracteres.")
        {
        }
    }
}
