using Prova3.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Results
{
    public class ResultWithSameStudentAndEvaluationAlreadyExistsException : BusinessException
    {
        public ResultWithSameStudentAndEvaluationAlreadyExistsException() : base("Resultado com mesmo aluno e avaliação ja exitentes")
        {
        }
    }
}
