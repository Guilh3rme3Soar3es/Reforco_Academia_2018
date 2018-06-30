using FluentAssertions;
using NUnit.Framework;
using Prova3.Application.Features.Results;
using Prova3.Common.Tests.Base;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using Prova3.Infra.Data.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Integration.Tests.Features.Results
{
    [TestFixture]
    public class ResultsIntegrationTests
    {
        private IResultRepository _resultRepository;
        private IResultService _service;
        private Student _student;
        private Evaluation _evaluation;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _resultRepository = new ResultRepository();
            _service = new ResultService(_resultRepository);

            _student = ObjectMother.GetExistentValidStudent();
            _evaluation = ObjectMother.GetExistentValidEvaluation();
        }

        [Test]
        public void Test_ResultIntegration_Add_SholdBeOk()
        {
            int expectedAmount = 4;
            int unrelatedId = 3;
            _student.Id = unrelatedId;
            Result resultToSave = ObjectMother.GetNewValidResult(_student,_evaluation);
            Result resultSaved = _service.Add(resultToSave);
            resultSaved.Id.Should().BeGreaterThan(0);
            IList<Result> results = _service.GetAll();
            results.Count.Should().Be(expectedAmount);
        }

        [Test]
        public void Test_ResultIntegration_Add_StudentInvalidNote_ShouldThrowException()
        {
            Result resultToSave = ObjectMother.GetInvalidResultWithNegativeNote(_student,_evaluation);
            Action action = () => _service.Add(resultToSave);
            action.Should().Throw<ResultNegativeNoteException>();
        }

        [Test]
        public void Test_ResultIntegration_Add_StudentNullStudent_ShouldThrowException()
        {
            Result resultToSave = ObjectMother.GetInvalidResultWithNullStudent(_evaluation);
            Action action = () => _service.Add(resultToSave);
            action.Should().Throw<ResultNullStudentException>();
        }

        [Test]
        public void Test_ResultIntegration_Add_StudentWithInvalidStudent_ShouldThrowException()
        {
            _student = ObjectMother.GetInvalidStudentWithInvalidAge();
            Result resultToSave = ObjectMother.GetNewValidResult(_student,_evaluation);
            Action action = () => _service.Add(resultToSave);
            action.Should().Throw<StudentInvalidAgeException>();
        }

        [Test]
        public void Test_ResultIntegration_Add_StudentNullEvaluation_ShouldThrowException()
        {
            Result resultToSave = ObjectMother.GetInvalidResultWithNullEvaluation(_student);
            Action action = () => _service.Add(resultToSave);
            action.Should().Throw<ResultNullEvaluationException>();
        }

        [Test]
        public void Test_ResultIntegration_Add_StudentWithInvalidEvaluation_ShouldThrowException()
        {
            _evaluation = ObjectMother.GetInvalidEvaluationWithUninformedSubject();
            Result resultToSave = ObjectMother.GetNewValidResult(_student,_evaluation);
            Action action = () => _service.Add(resultToSave);
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_ResultIntegration_Update_ShouldBeOK()
        {
            Result resultToUpdate = ObjectMother.GetExistentValidResult(_student,_evaluation);
            Result resultUpdated = _service.Update(resultToUpdate);
            resultUpdated.Note.Should().Be(resultToUpdate.Note);
            resultUpdated.Student.Id.Should().Be(resultToUpdate.Student.Id);
            resultUpdated.Evaluation.Id.Should().Be(resultToUpdate.Evaluation.Id);
        }

        [Test]
        public void Test_ResultIntegration_Update_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Result resultToUpdate = ObjectMother.GetNewValidResult(_student,_evaluation);
            resultToUpdate.Id = invalidId;
            Action action = () => _service.Update(resultToUpdate);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultIntegration_Update_StudentInvalidNote_ShouldThrowException()
        {
            int existentId = 1;
            Result resultToUpdate = ObjectMother.GetInvalidResultWithNegativeNote(_student, _evaluation);
            resultToUpdate.Id = existentId;
            Action action = () => _service.Update(resultToUpdate);
            action.Should().Throw<ResultNegativeNoteException>();
        }

        [Test]
        public void Test_ResultIntegration_Update_StudentNullStudent_ShouldThrowException()
        {
            int existentId = 1;
            Result resultToUpdate = ObjectMother.GetInvalidResultWithNullStudent(_evaluation);
            resultToUpdate.Id = existentId;
            Action action = () => _service.Update(resultToUpdate);
            action.Should().Throw<ResultNullStudentException>();
        }

        [Test]
        public void Test_ResultIntegration_Update_StudentWithInvalidStudent_ShouldThrowException()
        {
            _student = ObjectMother.GetInvalidStudentWithInvalidAge();
            Result resultToUpdate = ObjectMother.GetExistentValidResult(_student, _evaluation);
            Action action = () => _service.Update(resultToUpdate);
            action.Should().Throw<StudentInvalidAgeException>();
        }

        [Test]
        public void Test_ResultIntegration_Update_StudentNullEvaluation_ShouldThrowException()
        {
            int existentId = 1;
            Result resultToUpdate = ObjectMother.GetInvalidResultWithNullEvaluation(_student);
            resultToUpdate.Id = existentId;
            Action action = () => _service.Update(resultToUpdate);
            action.Should().Throw<ResultNullEvaluationException>();
        }

        [Test]
        public void Test_ResultIntegration_Update_StudentWithInvalidEvaluation_ShouldThrowException()
        {
            _evaluation = ObjectMother.GetInvalidEvaluationWithUninformedSubject();
            Result resultToUpdate = ObjectMother.GetExistentValidResult(_student, _evaluation);
            Action action = () => _service.Update(resultToUpdate);
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_StudentIntegration_Delete_ShouldBeOk()
        {
            Result resultToDelete = ObjectMother.GetExistentValidResult(_student,_evaluation);
            _service.Delete(resultToDelete);
            Result resultFound = _service.Get(resultToDelete.Id);
            resultFound.Should().BeNull();
        }

        [Test]
        public void Test_ResultIntegration_Delete_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Result resultToDelete = ObjectMother.GetExistentValidResult(_student,_evaluation);
            resultToDelete.Id = invalidId;
            Action action = () => _service.Delete(resultToDelete);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ReslultIntegration_Get_ShouldBeOk()
        {
            int existentId = 1;
            Result resultFound = _service.Get(existentId);
            resultFound.Should().NotBeNull();
            resultFound.Student.Should().NotBeNull();
            resultFound.Evaluation.Should().NotBeNull();
        }

        [Test]
        public void Test_ResultIntegration_Get_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _service.Get(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultIntegration_GetAll_ShouldBeOk()
        {
            int expectedamount = 3;
            IList<Result> results = _service.GetAll();
            results.Should().NotBeNullOrEmpty();
            results.First().Evaluation.Should().NotBeNull();
            results.First().Student.Should().NotBeNull();
            results.Count.Should().Be(expectedamount);
        }
    }
}
