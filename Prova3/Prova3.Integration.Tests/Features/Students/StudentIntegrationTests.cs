using FluentAssertions;
using NUnit.Framework;
using Prova3.Application.Features.Results;
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

namespace Prova3.Integration.Tests.Features.Students
{
    [TestFixture]
    public class StudentIntegrationTests
    {
        private IStudentRepository _studentRepository;
        private IResultRepository _resultRepository;
        private IStudentService _service;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTests.SeedDatabase();
            _studentRepository = new StudentRepository();
            _resultRepository = new ResultRepository();
            _service = new StudentService(_studentRepository,_resultRepository);
        }

        [Test]
        public void Test_StudentIntegration_Add_SholdBeOk()
        {
            int expectedAmount = 4;
            Student studentToSave = ObjectMother.GetNewValidStudent();
            Student studentSaved = _service.Add(studentToSave);
            studentSaved.Id.Should().BeGreaterThan(0);
            IList<Student> students = _service.GetAll();
            students.Count.Should().Be(expectedAmount);
            students.Last().Id.Should().Be(studentSaved.Id);
        }

        [Test]
        public void Test_StudentIntegration_Add_StudentUninformedName_ShouldThrowException()
        {
            Student studentToSave = ObjectMother.GetInvalidStudentWithUninformedName();
            Action action = () => _service.Add(studentToSave);
            action.Should().Throw<StudentUninformedNameException>();
        }

        [Test]
        public void Test_StudentIntegration_Add_StudentNameLengthOverflow_ShouldThrowException()
        {
            Student studentToSave = ObjectMother.GetInvalidStudentWithNameLengthOverflow();
            Action action = () => _service.Add(studentToSave);
            action.Should().Throw<StudentNameLengthOverflowException>();
        }

        [Test]
        public void Test_StudentIntegration_Add_StudentUninformedAge_ShouldThrowException()
        {
            Student studentToSave = ObjectMother.GetInvalidStudentWithUninformedAge();
            Action action = () => _service.Add(studentToSave);
            action.Should().Throw<StudentUninformedAgeException>();
        }

        [Test]
        public void Test_StudentIntegration_Add_StudentInvalidAge_ShouldThrowException()
        {
            Student studentToSave = ObjectMother.GetInvalidStudentWithInvalidAge();
            Action action = () => _service.Add(studentToSave);
            action.Should().Throw<StudentInvalidAgeException>();
        }

        [Test]
        public void Test_StudentIntegration_Update_ShouldBeOK()
        {
            Student studentToUpdate = ObjectMother.GetExistentValidStudent();
            Student studentUpdated = _service.Update(studentToUpdate);
            studentUpdated.Age.Should().Be(studentToUpdate.Age);
            studentUpdated.Name.Should().Be(studentToUpdate.Name);
            studentUpdated.Average.Should().Be(studentToUpdate.Average);
        }

        [Test]
        public void Test_StudentIntegration_Update_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Student studentToUpdate = ObjectMother.GetNewValidStudent();
            studentToUpdate.Id = invalidId;
            Action action = () => _service.Update(studentToUpdate);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_StudentIntegration_Update_StudentUninformedName_ShouldThrowException()
        {
            int existentId = 1;
            Student studentToUpdate = ObjectMother.GetInvalidStudentWithUninformedName();
            studentToUpdate.Id = existentId;
            Action action = () => _service.Update(studentToUpdate);
            action.Should().Throw<StudentUninformedNameException>();
        }

        [Test]
        public void Test_StudentIntegration_Update_StudentNameLengthOverflow_ShouldThrowException()
        {
            int existentId = 1;
            Student studentToUpdate = ObjectMother.GetInvalidStudentWithNameLengthOverflow();
            studentToUpdate.Id = existentId;
            Action action = () => _service.Update(studentToUpdate);
            action.Should().Throw<StudentNameLengthOverflowException>();
        }

        [Test]
        public void Test_StudentIntegration_Update_StudentUninformedAge_ShouldThrowException()
        {
            int existentId = 1;
            Student studentToUpdate = ObjectMother.GetInvalidStudentWithUninformedAge();
            studentToUpdate.Id = existentId;
            Action action = () => _service.Update(studentToUpdate);
            action.Should().Throw<StudentUninformedAgeException>();
        }

        [Test]
        public void Test_StudentIntegration_Update_StudentInvalidAge_ShouldThrowException()
        {
            int existentId = 1;
            Student studentToUpdate = ObjectMother.GetInvalidStudentWithInvalidAge();
            studentToUpdate.Id = existentId;
            Action action = () => _service.Update(studentToUpdate);
            action.Should().Throw<StudentInvalidAgeException>();
        }

        [Test]
        public void Test_StudentIntegration_Delete_ShouldBeOk()
        {
            int unrelatedId = 1;
            Student studentToDelete = ObjectMother.GetExistentValidStudent();
            studentToDelete.Id = unrelatedId;
            _service.Delete(studentToDelete);
            Student studentFound = _service.Get(studentToDelete.Id);
            studentFound.Should().BeNull();
        }

        [Test]
        public void Test_StudentIntegration_Delete_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Student studentToDelete = ObjectMother.GetExistentValidStudent();
            studentToDelete.Id = invalidId;
            Action action = () => _service.Delete(studentToDelete);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_StudentIntegration_Get_ShouldBeOk()
        {
            int existentId = 1;
            Student studentFound = _service.Get(existentId);
            studentFound.Should().NotBeNull();
            studentFound.Results.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_StudentIntegration_Get_InvalidId_ShouldThrowException()
        {
            int invalidId = 0;
            Action action = () => _service.Get(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_StudentIntegration_GetAll_ShouldBeOk()
        {
            int expectedamount = 3;
            IList<Student> students = _service.GetAll();
            students.Should().NotBeNullOrEmpty();
            students.First().Results.Should().NotBeNullOrEmpty();
            students.Count.Should().Be(expectedamount);
        }

        [Test]
        public void Test_StudentIntegration_CauclateAverage_ShouldBeOk()
        {
            int existentId = 2;
            int expectedAverage = 9;
            Student student = _service.Get(existentId);
            student.CalculateAverage();
            student.Average.Should().Be(expectedAverage);
        }

        [Test]
        public void Test_StudentIntegration_RoundAverageDown_ShouldBeOk()
        {
            int existentId = 1;
            int expectedAverage = 9;
            Student student = _service.Get(existentId);
            student.RoundAverage();
            student.Average.Should().Be(expectedAverage);
        }

        [Test]
        public void Test_StudentIntegration_RoundAverageToUp_ShouldBeOk()
        {
            int existentId = 2;
            int expectedAverage = 10;
            Student student = _service.Get(existentId);
            student.RoundAverage();
            student.Average.Should().Be(expectedAverage);
        }

        [Test]
        public void Test_StudentIntegration_RoundAverageToHalfPoint_ShouldBeOk()
        {
            int existentId = 3;
            double expectedAverage = 9.5;
            Student student = _service.Get(existentId);
            student.RoundAverage();
            student.Average.Should().Be(expectedAverage);
        }
    }
}
