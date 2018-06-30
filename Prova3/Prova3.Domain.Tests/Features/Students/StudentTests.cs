using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prova3.Common.Tests.Features.ObjectMothers;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Tests.Features.Students
{
    [TestFixture]
    public class StudentTests
    {
        private Mock<Result> _fakeFirstResult;
        private Mock<Result> _fakeSecondResult;
        private List<Result> _results;

        [SetUp]
        public void Initialize()
        {
        }
        [Test]
        public void Test_Student_Validate_ShouldBeOk()
        {
            
            Student student = ObjectMother.GetNewValidStudent();
            student.Validate();
        }

        [Test]
        public void Test_Student_ValidateWithUninformedStreetName_ShouldThrowException()
        {
            Student student = ObjectMother.GetInvalidStudentWithUninformedName();
            Action action = student.Validate;
            action.Should().Throw<StudentUninformedNameException>();
        }

        [Test]
        public void Test_Student_ValidateWithNameLengthOverflow_ShouldThrowException()
        {
            Student student = ObjectMother.GetInvalidStudentWithNameLengthOverflow();

            Action action = student.Validate;
            action.Should().Throw<StudentNameLengthOverflowException>();
        }

        [Test]
        public void Test_Student_ValidateWithUninformedAge_ShouldThrowException()
        {
            Student student = ObjectMother.GetInvalidStudentWithUninformedAge();

            Action action = student.Validate;
            action.Should().Throw<StudentUninformedAgeException>();
        }

        [Test]
        public void Test_Student_ValidateWithInvalidAge_ShouldThrowException()
        {
            Student student = ObjectMother.GetInvalidStudentWithInvalidAge();

            Action action = student.Validate;
            action.Should().Throw<StudentInvalidAgeException>();
        }

        [Test]
        public void Test_Student_CauculateAverage_ShouldBeOk()
        {
            double expectedAverage = 8;
            _fakeFirstResult = new Mock<Result>();
            _fakeSecondResult = new Mock<Result>();
            _results = new List<Result>()
            {
                _fakeFirstResult.Object,
                _fakeSecondResult.Object
            };
            Student student = ObjectMother.GetExistentValidStudent();
            student.Results = _results;
            _fakeFirstResult.Setup(r => r.Note).Returns(9);
            _fakeSecondResult.Setup(r => r.Note).Returns(7);

            student.CalculateAverage();

            student.Average.Should().Be(expectedAverage);
        }

        [Test]
        public void Teste_Student_RoundAverage_RoundedDown_ShouldBeOk()
        {
            double expectedAverage = 8;
            double average = 8.35;
            Student student = ObjectMother.GetExistentValidStudent();
            student.Average = average;

            student.RoundAverage();
            student.Average.Should().Be(expectedAverage);
        }

        [Test]
        public void Teste_Student_RoundAverage_RoundedUp_ShouldBeOk()
        {
            double expectedAverage = 9;
            double average = 8.75;
            Student student = ObjectMother.GetExistentValidStudent();
            student.Average = average;

            student.RoundAverage();
            student.Average.Should().Be(expectedAverage);
        }

        [Test]
        public void Teste_Student_RoundAverage_RoundedToHalfPoint_ShouldBeOk()
        {
            double expectedAverage = 8.5;
            double average = 8.74;
            Student student = ObjectMother.GetExistentValidStudent();
            student.Average = average;

            student.RoundAverage();
            student.Average.Should().Be(expectedAverage);
        }
    }
}
