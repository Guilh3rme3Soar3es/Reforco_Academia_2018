using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prova3.Application.Features.Students;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.ApplicationTests.Features.Students
{
    [TestFixture]
    public class StudentServiceTests
    {
        private Mock<IStudentRepository> _mockRepository;
        private Mock<IResultRepository> _mockResultRepository;
        private IStudentService _service;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<IStudentRepository>();
            _mockResultRepository = new Mock<IResultRepository>();
            _service = new StudentService(_mockRepository.Object, _mockResultRepository.Object);
        }

        [Test]
        public void Test_StudentService_Add_ShouldBeOk()
        {
            int studentExistentId = 1;
            Student studentToSave = ObjectMother.GetNewValidStudent();
            _mockRepository.Setup(r => r.Save(studentToSave)).Returns(ObjectMother.GetExistentValidStudent());

            Student studentReturned = _service.Add(studentToSave);

            studentReturned.Id.Should().Be(studentExistentId);
            _mockRepository.Verify(r => r.Save(studentToSave));
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_Add_InvalidStudent_ShouldThrowException()
        {
            Student studentToSave = ObjectMother.GetInvalidStudentWithInvalidAge();

            Action action = () => _service.Add(studentToSave);

            action.Should().Throw<StudentInvalidAgeException>();
            _mockResultRepository.VerifyNoOtherCalls();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_Update_ShouldBeOk()
        {
            Student studentToUpdate = ObjectMother.GetExistentValidStudent();
            _mockRepository.Setup(r => r.Update(studentToUpdate)).Returns(studentToUpdate);

            Student studentReturned = _service.Update(studentToUpdate);

            studentReturned.Should().Be(studentToUpdate);
            _mockResultRepository.VerifyNoOtherCalls();
            _mockRepository.Verify(r => r.Update(studentToUpdate));
        }

        [Test]
        public void Test_StudentService_Update_InvalidSender_ShouldThrowException()
        {
            int existentId = 1;
            Student studentToUpdate = ObjectMother.GetInvalidStudentWithInvalidAge();
            studentToUpdate.Id = existentId;

            Action action = () => _service.Update(studentToUpdate);

            action.Should().Throw<StudentInvalidAgeException>();
            _mockResultRepository.VerifyNoOtherCalls();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_Update_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Student studentToUpdate = ObjectMother.GetExistentValidStudent();
            studentToUpdate.Id = invalidId;

            Action action = () => _service.Update(studentToUpdate);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockResultRepository.VerifyNoOtherCalls();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_Delete_ShouldBeOk()
        {
            Student studentToDelete = ObjectMother.GetExistentValidStudent();
            studentToDelete.Results = new List<Result>() { ObjectMother.GetExistentValidResult(studentToDelete) };
            _mockResultRepository.Setup(sr => sr.GetByStudent(studentToDelete.Id)).Returns(new List<Result>() { ObjectMother.GetExistentValidResult(studentToDelete)});
            _mockResultRepository.Setup(sr => sr.Delete(studentToDelete.Results.First()));
            _mockRepository.Setup(sr => sr.Delete(studentToDelete));

            _service.Delete(studentToDelete);

            _mockRepository.Verify(sr => sr.Delete(studentToDelete));
            _mockResultRepository.Verify(sr => sr.Delete(studentToDelete.Results.First()));
        }

        [Test]
        public void Test_StudentService_Delete_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Student studentToDelete = ObjectMother.GetExistentValidStudent();
            studentToDelete.Id = invalidId;

            Action action  = () => _service.Delete(studentToDelete);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_Get_ShouldBeOk()
        {
            int existentId = 1;
            Student student = ObjectMother.GetExistentValidStudent();
            _mockRepository.Setup(sr => sr.Get(existentId)).Returns(ObjectMother.GetExistentValidStudent());
            _mockResultRepository.Setup(sr => sr.GetByStudent(existentId)).Returns(new List<Result>() { ObjectMother.GetExistentValidResult(student) });

            Student studentFound = _service.Get(existentId);

            studentFound.Id.Should().Be(existentId);
            _mockRepository.Verify(sr => sr.Get(existentId));
            _mockResultRepository.Verify(sr => sr.GetByStudent(existentId));
        }

        [Test]
        public void Test_StudentService_Get_NonExistentId_ShouldBeOk()
        {
            int existentId = 1;
            _mockRepository.Setup(sr => sr.Get(existentId));

            Student studentFound = _service.Get(existentId);

            studentFound.Should().BeNull();
            _mockRepository.Verify(sr => sr.Get(existentId));
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_Get_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            _mockRepository.Setup(sr => sr.Get(invalidId));

            Action action = () => _service.Get(invalidId);

            action.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
            _mockResultRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_StudentService_GetAll_ShouldBeOk()
        {
            int expectedAmount = 1;
            _mockRepository.Setup(sr => sr.GetAll()).Returns(new List<Student> { ObjectMother.GetExistentValidStudent() });

            List<Student> students = _service.GetAll().ToList();

            students.Count.Should().Be(expectedAmount);
            _mockRepository.Verify(sr => sr.GetAll());
        }
    }
}
