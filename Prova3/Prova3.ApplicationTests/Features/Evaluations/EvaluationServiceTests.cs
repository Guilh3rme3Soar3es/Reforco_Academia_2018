using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prova3.Application.Features.Evaluations;
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

namespace Prova3.ApplicationTests.Features.Evaluations
{
    [TestFixture]
    public class EvaluationServiceTests
    {
        private Mock<IEvaluationRepository> _mockEvaluationRepository;
        private Mock<IResultRepository> _mockResultRepository;
        private IEvaluationService _service;
        private Student _student;
        private Result _result;
        private List<Result> results;

        [SetUp]
        public void Initialize()
        {
            _mockEvaluationRepository = new Mock<IEvaluationRepository>();
            _mockResultRepository = new Mock<IResultRepository>();
            _service = new EvaluationService(_mockEvaluationRepository.Object, _mockResultRepository.Object);
        }

        [Test]
        public void Test_EvaluationService_Add_ShouldBeOk()
        {
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetExistentValidResult(_student, null);
            results = new List<Result> { _result };
            int ExistentId = 1;
            Evaluation evaluationToSave = ObjectMother.GetNewValidEvaluation(results);
            _result.Evaluation = evaluationToSave;
            _mockEvaluationRepository.Setup(r => r.Save(evaluationToSave)).Returns(ObjectMother.GetExistentValidEvaluation(results));
            _mockResultRepository.Setup(r => r.Save(evaluationToSave.Results.First()));

            Evaluation evaluationReturned = _service.Add(evaluationToSave);

            evaluationReturned.Id.Should().Be(ExistentId);
            _mockEvaluationRepository.Verify(r => r.Save(evaluationToSave));
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_Add_InvalidEvaluation_ShouldThrowException()
        {
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetNewValidResult(_student, null);
            results = new List<Result> { _result };
            Evaluation evaluationToSave = ObjectMother.GetInvalidEvaluationWithUninformedSubject(results);
            _result.Evaluation = evaluationToSave;
            _mockEvaluationRepository.Setup(r => r.Save(evaluationToSave)).Returns(ObjectMother.GetExistentValidEvaluation(results));
            _mockResultRepository.Setup(r => r.Save(evaluationToSave.Results.First()));

            Action action = () => _service.Add(evaluationToSave);

            action.Should().Throw<EvaluationUninformedSubjectException>();
            _mockEvaluationRepository.VerifyNoOtherCalls();
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_Update_ShouldBeOk()
        {
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetNewValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluationToUpdate = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluationToUpdate;
            _mockEvaluationRepository.Setup(r => r.Update(evaluationToUpdate)).Returns(ObjectMother.GetExistentValidEvaluation(results));
            _mockResultRepository.Setup(r => r.Update(evaluationToUpdate.Results.First()));

            Evaluation evaluationReturned = _service.Update(evaluationToUpdate);

            evaluationReturned.Should().Be(evaluationToUpdate);
            _mockEvaluationRepository.Verify(r => r.Update(evaluationToUpdate));
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_Update_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetNewValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluationToUpdate = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluationToUpdate;
            evaluationToUpdate.Id = invalidId;

            Action action = () => _service.Update(evaluationToUpdate);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockEvaluationRepository.VerifyNoOtherCalls();
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_Delete_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetNewValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluationToUpdate = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluationToUpdate;
            evaluationToUpdate.Id = invalidId;

            Action action = () => _service.Delete(evaluationToUpdate);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockEvaluationRepository.VerifyNoOtherCalls();
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_Delete_ShouldBeOk()
        {
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetExistentValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluationToDelete = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluationToDelete;
            _mockResultRepository.Setup(r => r.GetByEvaluation(evaluationToDelete.Id)).Returns(new List<Result>() { ObjectMother.GetExistentValidResult(_student, evaluationToDelete) });
            _mockEvaluationRepository.Setup(r => r.Delete(evaluationToDelete));
            _mockResultRepository.Setup(r => r.Delete(evaluationToDelete.Results.First()));

            _service.Delete(evaluationToDelete);

            _mockEvaluationRepository.Verify(r => r.Delete(evaluationToDelete));
            _mockResultRepository.Verify(r => r.Delete(evaluationToDelete.Results.First()));
        }

        [Test]
        public void Test_EvaluationService_Get_ShouldBeOk()
        {
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetExistentValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluation = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluation;
            _mockEvaluationRepository.Setup(r => r.Get(evaluation.Id)).Returns(evaluation);
            _mockResultRepository.Setup(r => r.GetByEvaluation(evaluation.Id)).Returns(results);

            Evaluation evaluationFound = _service.Get(evaluation.Id);

            evaluationFound.Should().Be(evaluation);
            evaluationFound.Results.Should().BeEquivalentTo(results);
            _mockEvaluationRepository.Verify(r => r.Get(evaluation.Id));
            _mockResultRepository.Verify(r => r.GetByEvaluation(evaluation.Id));
        }

        [Test]
        public void Test_EvaluationService_Get_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetNewValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluation = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluation;
            evaluation.Id = invalidId;

            Action action = () => _service.Get(evaluation.Id);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockEvaluationRepository.VerifyNoOtherCalls();
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_Get_NonexistentId_ShouldBeOk()
        {
            int nonexistentId = 100;
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetExistentValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluation = ObjectMother.GetExistentValidEvaluation(results);
            evaluation.Id = nonexistentId;
            _result.Evaluation = evaluation;
            _mockEvaluationRepository.Setup(r => r.Get(evaluation.Id));

            Evaluation evaluationFound = _service.Get(evaluation.Id);

            evaluationFound.Should().BeNull();
            _mockEvaluationRepository.Verify(r => r.Get(evaluation.Id));
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_EvaluationService_GetAll_ShouldBeOk()
        {
            _student = ObjectMother.GetExistentValidStudent();
            _result = ObjectMother.GetExistentValidResult(_student);
            results = new List<Result> { _result };
            Evaluation evaluation = ObjectMother.GetExistentValidEvaluation(results);
            _result.Evaluation = evaluation;
            _mockEvaluationRepository.Setup(r => r.GetAll()).Returns(new List<Evaluation> { evaluation });
            _mockResultRepository.Setup(r => r.GetByEvaluation(evaluation.Id)).Returns(results);

            List<Evaluation> evaluations = _service.GetAll().ToList();

            evaluations.First().Should().Be(evaluation);
            evaluations.First().Results.Should().BeEquivalentTo(results);
            _mockEvaluationRepository.Verify(r => r.GetAll());
            _mockResultRepository.Verify(r => r.GetByEvaluation(evaluation.Id));
        }
    }
}
