using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prova3.Application.Features.Results;
using Prova3.Application.Features.Students;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.ApplicationTests.Features.Results
{
    [TestFixture]
    public class ResultServiceTests
    {
        private Mock<IResultRepository> _mockRepository;
        private IResultService _service;

        private Evaluation _evaluation;
        private Student _student;
        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<IResultRepository>();
            _service = new ResultService(_mockRepository.Object);
            _evaluation = new Evaluation();
            _student = new Student();
        }

        [Test]
        public void Test_ResultService_Add_ShouldBeOk()
        {
            int studentExistentId = 1;
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToSave = ObjectMother.GetNewValidResult(_student,_evaluation);
            _mockRepository.Setup(r => r.Save(resultToSave)).Returns(ObjectMother.GetExistentValidResult(_student,_evaluation));
            _mockRepository.Setup(r => r.GetByEvaluationAndStudent(resultToSave)).Returns(new List<Result>());

            Result resultReturned = _service.Add(resultToSave);

            resultReturned.Id.Should().Be(studentExistentId);
            resultReturned.Evaluation.Should().Be(_evaluation);
            _mockRepository.Verify(r => r.Save(resultToSave));
        }

        [Test]
        public void Test_ResultService_Add_InvalidResult_ShouldThrowException()
        {
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToSave = ObjectMother.GetInvalidResultWithNegativeNote(_student,_evaluation);

            Action action = () => _service.Add(resultToSave);

            action.Should().Throw<ResultNegativeNoteException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ResultService_Add_ResultWithSameStudentAndEvaluationAlreadyExists_ShouldThrowException()
        {
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToSave = ObjectMother.GetNewValidResult(_student, _evaluation);
            _mockRepository.Setup(r => r.GetByEvaluationAndStudent(resultToSave)).Returns(new List<Result>() { ObjectMother.GetExistentValidResult(_student)});


            Action action = () => _service.Add(resultToSave);

            action.Should().Throw<ResultWithSameStudentAndEvaluationAlreadyExistsException>();
        }

        [Test]
        public void Test_ResultService_Update_ShouldBeOk()
        {
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToUpdate = ObjectMother.GetExistentValidResult(_student,_evaluation);
            _mockRepository.Setup(r => r.Update(resultToUpdate)).Returns(resultToUpdate);

            Result resultReturned = _service.Update(resultToUpdate);

            resultReturned.Should().Be(resultToUpdate);
            _mockRepository.Verify(r => r.Update(resultToUpdate));
        }

        [Test]
        public void Test_ResultService_Update_InvalidResult_ShouldThrowException()
        {
            int existentId = 1;
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToUpdate = ObjectMother.GetInvalidResultWithNegativeNote(_student,_evaluation);
            resultToUpdate.Id = existentId;

            Action action = () => _service.Update(resultToUpdate);

            action.Should().Throw<ResultNegativeNoteException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ResultService_Update_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToUpdate = ObjectMother.GetExistentValidResult(_student,_evaluation);
            resultToUpdate.Id = invalidId;

            Action action = () => _service.Update(resultToUpdate);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ResultService_Delete_ShouldBeOk()
        {
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToDelete = ObjectMother.GetExistentValidResult(_student,_evaluation);
            _mockRepository.Setup(sr => sr.Delete(resultToDelete));

            _service.Delete(resultToDelete);

            _mockRepository.Verify(sr => sr.Delete(resultToDelete));
        }

        [Test]
        public void Test_ResultService_Delete_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            Result resultToDelete = ObjectMother.GetExistentValidResult(_student,_evaluation);
            resultToDelete.Id = invalidId;

            Action action = () => _service.Delete(resultToDelete);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ResultService_Get_ShouldBeOk()
        {
            int existentId = 1;
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            _mockRepository.Setup(sr => sr.Get(existentId)).Returns(ObjectMother.GetExistentValidResult(_student,_evaluation));

            Result resultFound = _service.Get(existentId);

            resultFound.Id.Should().Be(existentId);
            _mockRepository.Verify(sr => sr.Get(existentId));
        }

        [Test]
        public void Test_ResultService_Get_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _mockRepository.Setup(sr => sr.Get(invalidId));

            Action action = () => _service.Get(invalidId);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ResultService_GetAll_ShouldBeOk()
        {
            int expectedAmount = 1;
            _evaluation = ObjectMother.GetExistentValidEvaluation();
            _student = ObjectMother.GetExistentValidStudent();
            _mockRepository.Setup(sr => sr.GetAll()).Returns(new List<Result> { ObjectMother.GetExistentValidResult(_student,_evaluation) });

            List<Result> results = _service.GetAll().ToList(); 

            results.Count.Should().Be(expectedAmount);
            results.First().Evaluation.Should().Be(_evaluation);
            results.First().Student.Should().Be(_student);
            _mockRepository.Verify(sr => sr.GetAll());
        }
    }
}
