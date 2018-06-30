using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Evaluations
{
    public class EvaluationResulstWithEqualsStudentsException : BusinessException
    {
        public EvaluationResulstWithEqualsStudentsException() : base("Avaliação com um ou mais resultados para o mesmo aluno.")
        {
        }
    }
}
