using FluentAssertions;
using NUnit.Framework;
using Prova3.Application.Features.Students;
using Prova3.Common.Tests.Base;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using Prova3.Infra.Data.Features.Results;
using Prova3.Infra.Data.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Infra.Data.Tests.Features.Students
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        private IResultRepository _resultRepository;
        private IStudentRepository _repository;
        private IStudentService _service;
        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _resultRepository = new ResultRepository();
            _repository = new StudentRepository();
            _service = new StudentService(_repository, _resultRepository);
        }

        [Test]
        public void Test_StudentRepository_Save_ShouldBeOk()
        {
            Student studentToSave = ObjectMother.GetNewValidStudent();
            Student studentSaved = _repository.Save(studentToSave);
            studentSaved.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Test_StudentRepository_SaveInvalid_ShouldThrowException()
        {
            Student studentToSave = ObjectMother.GetInvalidStudentWithUninformedName();
            Action action = () => _repository.Save(studentToSave);
            action.Should().Throw<StudentUninformedNameException>();
        }

        [Test]
        public void Test_StudentRepository_Update_ShouldBeOk()
        {
            Student studentToUpdate = ObjectMother.GetExistentValidStudent();

            studentToUpdate = _repository.Update(studentToUpdate);

            Student result = _repository.Get(studentToUpdate.Id);
            result.Should().NotBeNull();
            result.Name.Should().Be(studentToUpdate.Name);
        }

        [Test]
        public void Test_StudentRepository_UpdateInvalid_ShouldThrowException()
        {
            int existentId = 1;
            Student evaluationToUpdate = ObjectMother.GetInvalidStudentWithUninformedName();
            evaluationToUpdate.Id = existentId;

            Action action = () => _repository.Update(evaluationToUpdate);
            action.Should().Throw<StudentUninformedNameException>();
        }

        [Test]
        public void Test_StudentRepository_UpdateInvalidId_ShouldThrowException()
        {
            Student studentToUpdate = ObjectMother.GetNewValidStudent();

            Action action = () => _repository.Update(studentToUpdate);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_StudentRepository_DeleteInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Student studentToDelete = ObjectMother.GetNewValidStudent();
            studentToDelete.Id = invalidId;
            Action action = () => _repository.Delete(studentToDelete);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_StudentRepository_Delete_ShouldBeOk()
        {
            int existentId = 3;
            Student studentToDelete = ObjectMother.GetExistentValidStudent();
            studentToDelete.Id = existentId;
            _repository.Delete(studentToDelete);
            Student result = _repository.Get(studentToDelete.Id);
            result.Should().BeNull();
        }

        [Test]
        public void Test_StudentRepository_Get_ShouldBeOk()
        {
            int existentId = 1;
            Student studentFound = _repository.Get(existentId);
            studentFound.Should().NotBeNull();
        }

        [Test]
        public void Test_StudentRepository_GetInvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _repository.Get(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_StudentRepository_GetAll_ShouldBeOk()
        {
            int expectedAmount = 3;
            List<Student> students = _repository.GetAll().ToList();
            students.Should().NotBeNullOrEmpty();
            students.Count.Should().Be(expectedAmount);
        }
    }
}
