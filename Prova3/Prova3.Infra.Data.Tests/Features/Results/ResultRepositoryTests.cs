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

namespace Prova3.Infra.Data.Tests.Features.Results
{
    [TestFixture]
    public class ResultRepositoryTests
    {
        private IResultRepository _repository;
        private IResultService _service;

        private Student _student;
        private Evaluation _evaluation;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _repository = new ResultRepository();
            _service = new ResultService(_repository);

            _student = ObjectMother.GetExistentValidStudent();
            _evaluation = ObjectMother.GetExistentValidEvaluation();
        }

        [Test]
        public void Test_ResultRepository_Save_ShouldBeOk()
        {
            Result resultToSave = ObjectMother.GetNewValidResult(_student, _evaluation);
            Result resultSaved = _repository.Save(resultToSave);
            resultSaved.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Test_ResultRepository_SaveInvalid_ShouldThrowException()
        {
            Result resultToSave = ObjectMother.GetInvalidResultWithNegativeNote(_student);
            Action action = () => _repository.Save(resultToSave);
            action.Should().Throw<ResultNegativeNoteException>();
        }

        [Test]
        public void Test_ResultRepository_Update_ShouldBeOk()
        {
            Result resultToUpdate = ObjectMother.GetExistentValidResult(_student, _evaluation);

            resultToUpdate = _repository.Update(resultToUpdate);

            Result result = _repository.Get(resultToUpdate.Id);
            result.Should().NotBeNull();
            result.Note.Should().Be(resultToUpdate.Note);
        }

        [Test]
        public void Test_ResultRepository_UpdateInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Result resultToUpdate = ObjectMother.GetNewValidResult(_student);
            resultToUpdate.Id = invalidId;
            Action action = () => _repository.Update(resultToUpdate);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultRepository_UpdateInvalid_ShouldThrowException()
        {
            int existentId = 1;
            Result resultToUpdate = ObjectMother.GetInvalidResultWithNegativeNote(_student);
            resultToUpdate.Id = existentId;

            Action action = () => _repository.Update(resultToUpdate);
            action.Should().Throw<ResultNegativeNoteException>();
        }

        [Test]
        public void Test_ResultRepository_Delete_ShouldBeOk()
        {
            int existentId = 1;
            Result resultToDelete = ObjectMother.GetExistentValidResult(_student);
            resultToDelete.Id = existentId;
            _repository.Delete(resultToDelete);
            Result result = _repository.Get(resultToDelete.Id);
            result.Should().BeNull();
        }

        [Test]
        public void Test_ResultRepository_DeleteInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Result resultToDelete = ObjectMother.GetNewValidResult(_student);
            resultToDelete.Id = invalidId;
            Action action = () => _repository.Delete(resultToDelete);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultRepository_Get_ShouldBeOk()
        {
            int existentId = 1;
            Result resultFound = _repository.Get(existentId);
            resultFound.Should().NotBeNull();
        }

        [Test]
        public void Test_ResultRepository_GetInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _repository.Get(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultRepository_GetAll_ShouldBeOk()
        {
            int expectedAmount = 3;
            List<Result> results = _repository.GetAll().ToList();
            results.Should().NotBeNullOrEmpty();
            results.Count.Should().Be(expectedAmount);
        }

        [Test]
        public void Test_ResultRepository_GetByEvaluation_ShouldBeOk()
        {
            int existentId = 2;
            int expectedAmount = 1;
            List<Result> results = _repository.GetByEvaluation(existentId).ToList();
            results.Should().NotBeNullOrEmpty();
            results.Count.Should().Be(expectedAmount);
        }

        [Test]
        public void Test_ResultRepository_GetByEvluationInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _repository.GetByEvaluation(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultRepository_GetByStudent_ShouldBeOk()
        {
            int existentId = 1;
            int expectedAmount = 1;
            List<Result> results = _repository.GetByStudent(existentId).ToList();
            results.Should().NotBeNullOrEmpty();
            results.Count.Should().Be(expectedAmount);
        }

        [Test]
        public void Test_ResultRepository_GetByStudentInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _repository.GetByStudent(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_ResultRepository_GetByEvaluationAndStudent_ShouldBeOk()
        {
            int expectedAmount = 1;
            Evaluation evaluation = ObjectMother.GetExistentValidEvaluation();
            Result result = ObjectMother.GetExistentValidResult(_student,evaluation);

            List<Result> results = _repository.GetByEvaluationAndStudent(result).ToList();
            results.Should().NotBeNullOrEmpty();
            results.Count.Should().Be(expectedAmount);
        }
    }
}
