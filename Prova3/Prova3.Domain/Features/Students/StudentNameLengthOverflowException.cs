using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Students
{
    public class StudentNameLengthOverflowException : BusinessException
    {
        public StudentNameLengthOverflowException() : base("Aluno com nome maior que 100 caracteres.")
        {
        }
    }
}
