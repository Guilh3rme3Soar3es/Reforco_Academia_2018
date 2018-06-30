using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Students
{
    public class StudentUninformedAgeException : BusinessException
    {
        public StudentUninformedAgeException() : base("Aluno com idade não informada.")
        {
        }
    }
}
