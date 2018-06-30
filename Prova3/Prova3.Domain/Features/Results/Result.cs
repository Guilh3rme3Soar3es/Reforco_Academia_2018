using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Results
{
    public class Result
    {
        public int Id { get; set; }
        public virtual double Note { get; set; }
        public Evaluation Evaluation { get; set; }
        public virtual Student Student { get; set; }

        public virtual void Validate()
        {
            if (Note < 0)
                throw new ResultNegativeNoteException();
            if (Student == null)
                throw new ResultNullStudentException();
            Student.Validate();
            if (Evaluation == null)
                throw new ResultNullEvaluationException();
            Evaluation.Validate();
        }
    }
}
