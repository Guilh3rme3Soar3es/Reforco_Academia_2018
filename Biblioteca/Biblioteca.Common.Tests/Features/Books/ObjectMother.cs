using Biblioteca.Domain.Features.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Book GetBookOK()
        {
            return new Book
            {
                Id = 1,
                Author = "João da Silva",
                Theme = "Test Driven Development",
                PostDate = DateTime.Now.AddDays(-1),
                Title = "Como praticar TDD",
                Volume = 1,
                IsAvaliable = true,
            };
        }
    }
}
