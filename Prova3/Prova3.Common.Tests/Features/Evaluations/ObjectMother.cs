using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Evaluation GetNewValidEvaluation(List<Result> results = null)
        {
            return new Evaluation
            {
                Date = DateTime.Now.AddDays(-1),
                Subject = "TDD",
                Results = results
            };
        }

        public static Evaluation GetNewValidEvaluationNoresults()
        {
            return new Evaluation
            {
                Date = DateTime.Now.AddDays(-1),
                Subject = "TDD",
                Results = new List<Result>()
            };
        }

        public static Evaluation GetExistentValidEvaluation(List<Result> results = null)
        {
            return new Evaluation
            {
                Id = 1,
                Date = DateTime.Now.AddDays(-1),
                Subject = "TDD",
                Results = results
            };
        }

        public static Evaluation GetInvalidEvaluationWithUninformedSubject(List<Result> results = null)
        {
            return new Evaluation
            {
                Date = DateTime.Now.AddDays(-1),
                Subject = null,
                Results = results
            };
        }

        public static Evaluation GetInvalidEvaluationWithSubjectLengthOverflow(List<Result> results = null)
        {
            return new Evaluation
            {
                Date = DateTime.Now.AddDays(-1),
                Subject = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab",
                Results = results
            };
        }

        public static Evaluation GetInvalidEvaluationResulstWithEqualsStudents(List<Result> InvalidResults = null)
        {
            return new Evaluation
            {
                Date = DateTime.Now.AddDays(-1),
                Subject = "TDD",
                Results = InvalidResults
            };
        }
    }
}
