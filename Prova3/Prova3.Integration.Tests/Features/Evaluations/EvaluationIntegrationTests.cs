using FluentAssertions;
using NUnit.Framework;
using Prova3.Application.Features.Evaluations;
using Prova3.Common.Tests.Base;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using Prova3.Infra.Data.Features.Evaluations;
using Prova3.Infra.Data.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prova3.Integration.Tests.Features.Evaluations
{
    [TestFixture]
    public class EvaluationIntegrationTests
    {
        private IEvaluationRepository _evaluationRepository;
        private IResultRepository _resultRepository;
        private IEvaluationService _service;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _evaluationRepository = new EvaluationRepository();
            _resultRepository = new ResultRepository();
            _service = new EvaluationService(_evaluationRepository, _resultRepository);
        }

        [Test]
        public void Test_EvaluationsIntegration_Add_SholdBeOk()
        {
            int expectedAmount = 5;
            Evaluation evaluationToSave = ObjectMother.GetNewValidEvaluation();
            Evaluation evaluationSaved = _service.Add(evaluationToSave);
            evaluationSaved.Id.Should().BeGreaterThan(0);
            IList<Evaluation> evaluations = _service.GetAll();
            evaluations.Count.Should().Be(expectedAmount);
            evaluations.Last().Id.Should().Be(evaluationSaved.Id);
        }

        [Test]
        public void Test_EvaluationIntegration_Add_EvaluationUninformedSubject_ShouldThrowException()
        {
            Evaluation evaluationToSave = ObjectMother.GetInvalidEvaluationWithUninformedSubject();
            Action action = () => _service.Add(evaluationToSave);
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_EvaluationIntegration_Add_EvaluationsSubjectLengthOverflow_ShouldThrowException()
        {
            Evaluation evaluationToSave = ObjectMother.GetInvalidEvaluationWithSubjectLengthOverflow();
            Action action = () => _service.Add(evaluationToSave);
            action.Should().Throw<EvaluationSubjectLengthOverflowException>();
        }

        [Test]
        public void Test_EvaluationIntegration_Update_ShouldBeOK()
        {
            Evaluation evaluationToUpdate = ObjectMother.GetExistentValidEvaluation();
            Evaluation evaluationUpdated = _service.Update(evaluationToUpdate);
            evaluationUpdated.Date.Should().Be(evaluationToUpdate.Date);
            evaluationUpdated.Subject.Should().Be(evaluationToUpdate.Subject);
        }

        [Test]
        public void Test_EvaluationIntegration_Update_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Evaluation evaluationToUpdate = ObjectMother.GetNewValidEvaluation();
            evaluationToUpdate.Id = invalidId;
            Action action = () => _service.Update(evaluationToUpdate);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_EvaluationIntegration_Update_EvaluationUninformedSubject_ShouldThrowException()
        {
            int existentId = 1;
            Evaluation evaluationToUpdate = ObjectMother.GetInvalidEvaluationWithUninformedSubject();
            evaluationToUpdate.Id = existentId;
            Action action = () => _service.Update(evaluationToUpdate);
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_EvaluationIntegration_Update_EvaluationsSubjectLengthOverflow_ShouldThrowException()
        {
            int existentId = 1;
            Evaluation evaluationToUpdate = ObjectMother.GetInvalidEvaluationWithSubjectLengthOverflow();
            evaluationToUpdate.Id = existentId;
            Action action = () => _service.Update(evaluationToUpdate);
            action.Should().Throw<EvaluationSubjectLengthOverflowException>();
        }

        [Test]
        public void Test_EvaluationIntegration_Delete_ShouldBeOk()
        {
            Evaluation evaluationToDelete = ObjectMother.GetExistentValidEvaluation();
            _service.Delete(evaluationToDelete);
            Evaluation evaluationFound = _service.Get(evaluationToDelete.Id);
            evaluationFound.Should().BeNull();
        }

        [Test]
        public void Test_EvaluationIntegration_Delete_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Evaluation evaluationToDelete = ObjectMother.GetExistentValidEvaluation();
            evaluationToDelete.Id = invalidId;
            Action action = () => _service.Delete(evaluationToDelete);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_EvaluationIntegration_Get_ShouldBeOk()
        {
            int existentId = 2;
            Evaluation evaluationFound = _service.Get(existentId);
            evaluationFound.Should().NotBeNull();
            evaluationFound.Results.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_EvaluationIntegration_Get_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _service.Get(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_EvaluationIntegration_GetAll_ShouldBeOk()
        {
            int expectedamount = 4;
            IList<Evaluation> evaluations = _service.GetAll();
            evaluations.Should().NotBeNullOrEmpty();
            evaluations.First().Results.Should().NotBeNullOrEmpty();
            evaluations.Count.Should().Be(expectedamount);
        }
    }
}
