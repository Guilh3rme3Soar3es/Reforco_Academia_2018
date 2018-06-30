using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Students
{
    public class StudentUninformedNameException : BusinessException
    {
        public StudentUninformedNameException() : base("Aluno com nome não informado.")
        {
        }
    }
}
