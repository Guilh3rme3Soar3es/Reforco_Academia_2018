using Biblioteca.Domain.Features.Books;
using Biblioteca.Domain.Features.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Loan GetLoanOk(Book book)
        {
            return new Loan
            {
                Id = 1,
                ClientName = "José da Silva",
                DateDevolution = DateTime.Now.AddDays(+1),
                Book = book
            };
        }
    }
}
