using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Evaluations
{
    public class EvaluationUninformedSubjectException : BusinessException
    {
        public EvaluationUninformedSubjectException() : base("Avaliação com assunto não informado.")
        {
        }
    }
}
