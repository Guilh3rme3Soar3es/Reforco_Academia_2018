using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Results
{
    public class ResultNullEvaluationException : BusinessException
    {
        public ResultNullEvaluationException() : base("Resultado com avaliação não informada.")
        {
        }
    }
}
