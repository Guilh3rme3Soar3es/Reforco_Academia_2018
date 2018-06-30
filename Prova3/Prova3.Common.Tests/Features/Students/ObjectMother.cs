using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Student GetNewValidStudent()
        {
            return new Student
            {
                Age = 18,
                Name = "João da Silva"
            };
        } 

        public static Student GetExistentValidStudent()
        {
            return new Student
            {
                Id = 1,
                Age = 18,
                Name = "João da Silva"
            };
        }

        public static Student GetInvalidStudentWithUninformedName()
        {
            return new Student
            {
                Age = 18,
                Name = null
            };
        }

        public static Student GetInvalidStudentWithNameLengthOverflow()
        {
            return new Student
            {
                Age = 18,
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab"
            };
        }

        public static Student GetInvalidStudentWithUninformedAge()
        {
            return new Student
            {
                Name = "João da Silva"
            };
        }

        public static Student GetInvalidStudentWithInvalidAge()
        {
            return new Student
            {
                Age = -1,
                Name = "João da Silva"
            };
        }
    }
}
