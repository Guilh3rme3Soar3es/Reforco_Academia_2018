using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Tests.Features.Evaluations
{
    [TestFixture]
    public class EvaluationTests
    {
        private Mock<Result> _fakeResult;
        private Mock<Result> _fakeDuplicatedResult;
        private List<Result> results;

        [SetUp]
        public void Initialize()
        {
            _fakeResult = new Mock<Result>();
            _fakeDuplicatedResult = new Mock<Result>();
            results = new List<Result> { _fakeResult.Object };
        }

        [Test]
        public void Test_Evaluation_ValidateEvaluationWithResults_ShouldBeOk()
        {
            int validId = 1;
            _fakeResult.Setup(result => result.Student.Id).Returns(validId);
            Evaluation evaluation = ObjectMother.GetNewValidEvaluation(results);
            evaluation.Validate();
        }

        [Test]
        public void Test_Evaluation_ValidateEvaluationNoResults_ShouldBeOk()
        {
            Evaluation evaluation = ObjectMother.GetNewValidEvaluationNoresults();
            evaluation.Validate();
        }

        [Test]
        public void Test_Evaluation_ValidateWithUninformedSubject_ShouldThrowException()
        {
            int validId = 1;
            _fakeResult.Setup(result => result.Student.Id).Returns(validId);
            Evaluation evaluation = ObjectMother.GetInvalidEvaluationWithUninformedSubject(results);
            Action action = evaluation.Validate;
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_Evaluation_ValidateWithSubjectLengthOverflow_ShouldThrowException()
        {
            int validId = 1;
            _fakeResult.Setup(result => result.Student.Id).Returns(validId);
            Evaluation evaluation = ObjectMother.GetInvalidEvaluationWithSubjectLengthOverflow(results);
            Action action = evaluation.Validate;
            action.Should().Throw<EvaluationSubjectLengthOverflowException>();
        }

        [Test]
        public void Test_Evaluation_ValidateResulstWithEqualsStudents_ShouldThrowException()
        {
            int duplicatedId = 1;
            _fakeResult.Setup(result => result.Student.Id).Returns(duplicatedId);
            _fakeDuplicatedResult.Setup(result => result.Student.Id).Returns(duplicatedId);
            results.Add(_fakeDuplicatedResult.Object);
            Evaluation evaluation = ObjectMother.GetInvalidEvaluationResulstWithEqualsStudents(results);
            Action action = evaluation.Validate;
            action.Should().Throw<EvaluationResulstWithEqualsStudentsException>();
        }
    }
}
