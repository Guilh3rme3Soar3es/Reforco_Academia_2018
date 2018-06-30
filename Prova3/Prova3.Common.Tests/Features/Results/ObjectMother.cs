using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Result GetNewValidResult(Student student, Evaluation evaluation = null)
        {
            return new Result
            {
                Note = 10,
                Student = student,
                Evaluation = evaluation
            };
        }

        public static Result GetExistentValidResult(Student student, Evaluation evaluation = null)
        {
            return new Result
            {
                Id = 1,
                Note = 100,
                Student = student,
                Evaluation = evaluation
            };
        }

        public static Result GetInvalidResultWithNegativeNote(Student student, Evaluation evaluation = null)
        {
            return new Result
            {
                Note = -1,
                Student = student,
                Evaluation = evaluation
            };
        }

        public static Result GetInvalidResultWithNullStudent(Evaluation evaluation)
        {
            return new Result
            {
                Note = 10,
                Student = null,
                Evaluation = evaluation
            };
        }

        public static Result GetInvalidResultWithNullEvaluation(Student student)
        {
            return new Result
            {
                Note = 10,
                Student = student,
                Evaluation = null
            };
        }
    }
}
