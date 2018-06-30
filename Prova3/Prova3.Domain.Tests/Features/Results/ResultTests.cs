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

namespace Prova3.Domain.Tests.Features.Results
{
    [TestFixture]
    public class ResultTests
    {
        private Mock<Student> _fakeStudent;
        private Mock<Evaluation> _fakeEvaluation;

        [SetUp]
        public void Initialize()
        {
            _fakeStudent = new Mock<Student>();
            _fakeEvaluation = new Mock<Evaluation>();

        }
        [Test]
        public void Test_Result_Validate_ShouldBeOk()
        {
            _fakeStudent.Setup(student => student.Validate());
            _fakeEvaluation.Setup(student => student.Validate());
            Result result = ObjectMother.GetNewValidResult(_fakeStudent.Object, _fakeEvaluation.Object);
            result.Validate();
        }

        [Test]
        public void Test_Result_ValidateWithNegativeNote_ShouldThrowException()
        {
            Result result = ObjectMother.GetInvalidResultWithNegativeNote(_fakeStudent.Object, _fakeEvaluation.Object);
            Action action = result.Validate;
            action.Should().Throw<ResultNegativeNoteException>();
        }

        [Test]
        public void Test_Result_ValidateWithNullStudent_ShouldThrowException()
        {
            Result result = ObjectMother.GetInvalidResultWithNullStudent(_fakeEvaluation.Object);
            Action action = result.Validate;
            action.Should().Throw<ResultNullStudentException>();
        }

        [Test]
        public void Test_Result_ValidateWithNullEvaluation_ShouldThrowException()
        {
            Result result = ObjectMother.GetInvalidResultWithNullEvaluation(_fakeStudent.Object);
            Action action = result.Validate;
            action.Should().Throw<ResultNullEvaluationException>();
        }

        [Test]
        public void Test_Result_ValidateWithInvalidStudent_ShouldThrowException()
        {
            _fakeStudent.Setup(student => student.Validate()).Throws<StudentInvalidAgeException>();
            Result result = ObjectMother.GetNewValidResult(_fakeStudent.Object, _fakeEvaluation.Object);
            Action action = result.Validate;
            action.Should().Throw<StudentInvalidAgeException>();
        }

        [Test]
        public void Test_Result_ValidateWithInvalidEvaluation_ShouldThrowException()
        {
            _fakeEvaluation.Setup(student => student.Validate()).Throws<EvaluationUninformedSubjectException>();
            Result result = ObjectMother.GetNewValidResult(_fakeStudent.Object, _fakeEvaluation.Object);
            Action action = result.Validate;
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }
    }
}
