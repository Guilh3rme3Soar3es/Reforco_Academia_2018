using Prova3.Domain.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Evaluations
{
    public class Evaluation
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public List<Result> Results { get; set; }

        public virtual void Validate()
        {
            if (String.IsNullOrEmpty(Subject))
                throw new EvaluationUninformedSubjectException();
            if (Subject.Length > 100)
                throw new EvaluationSubjectLengthOverflowException();
            if (Results != null && Results.Count > 0)
            {
                if (Results.GroupBy(a => a.Student.Id).Any(a => a.Count() > 1))
                    throw new EvaluationResulstWithEqualsStudentsException();
            }
        }
    }
}
