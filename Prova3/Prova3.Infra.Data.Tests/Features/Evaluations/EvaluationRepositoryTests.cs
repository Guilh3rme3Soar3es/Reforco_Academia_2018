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
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Infra.Data.Tests.Features.Evaluations
{
    [TestFixture]
    public class EvaluationRepositoryTests
    {
        private IEvaluationRepository _repository;
        private IResultRepository _resultRepository;

        private IEvaluationService _service;
        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _repository = new EvaluationRepository();
            _resultRepository = new ResultRepository();
            _service = new EvaluationService(_repository,_resultRepository);
        }

        [Test]
        public void Test_EvaluationRepository_Save_ShouldBeOk()
        {
            Evaluation evaluationToSave = ObjectMother.GetNewValidEvaluation();
            Evaluation evaluationSaved = _repository.Save(evaluationToSave);
            evaluationSaved.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Test_EvaluationRepository_SaveInvalid_ShouldThrowException()
        {
            Evaluation evaluationToSave = ObjectMother.GetInvalidEvaluationWithUninformedSubject();
            Action action = () => _repository.Save(evaluationToSave);
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_EvaluationRepository_Update_ShouldBeOk()
        {
            Evaluation evaluationToUpdate = ObjectMother.GetExistentValidEvaluation();

            evaluationToUpdate = _repository.Update(evaluationToUpdate);

            Evaluation result = _repository.Get(evaluationToUpdate.Id);
            result.Should().NotBeNull();
            result.Subject.Should().Be(evaluationToUpdate.Subject);
        }

        [Test]
        public void Test_EvaluationRepository_UpdateInvalidId_ShouldThrowException()
        {
            Evaluation evaluationToUpdate = ObjectMother.GetNewValidEvaluation();

            Action action = () => _repository.Update(evaluationToUpdate);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_EvaluationRepository_UpdateInvalid_ShouldThrowException()
        {
            int existentId = 1;
            Evaluation evaluationToUpdate = ObjectMother.GetInvalidEvaluationWithUninformedSubject();
            evaluationToUpdate.Id = existentId;

            Action action = () => _repository.Update(evaluationToUpdate);
            action.Should().Throw<EvaluationUninformedSubjectException>();
        }

        [Test]
        public void Test_EvaluationRepository_DeleteInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Evaluation evaluationToDelete = ObjectMother.GetNewValidEvaluation();
            evaluationToDelete.Id = invalidId;
            Action action = () => _repository.Delete(evaluationToDelete);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_EvaluationRepository_Delete_ShouldBeOk()
        {
            int existentId = 4;
            Evaluation evaluationToDelete = ObjectMother.GetExistentValidEvaluation();
            evaluationToDelete.Id = existentId;
            _repository.Delete(evaluationToDelete);
            Evaluation result = _repository.Get(evaluationToDelete.Id);
            result.Should().BeNull();
        }

        [Test]
        public void Test_EvaluationRepository_Get_ShouldBeOk()
        {
            int existentId = 2;
            Evaluation evaluationFound = _repository.Get(existentId);
            evaluationFound.Should().NotBeNull();
        }

        [Test]
        public void Test_EvaluationRepository_GetInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _repository.Get(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_EvaluationRepository_GetAll_ShouldBeOk()
        {
            int expectedAmount = 4;
            List<Evaluation> evaluations = _repository.GetAll().ToList();
            evaluations.Should().NotBeNullOrEmpty();
            evaluations.Count.Should().Be(expectedAmount);
        }
    }
}
