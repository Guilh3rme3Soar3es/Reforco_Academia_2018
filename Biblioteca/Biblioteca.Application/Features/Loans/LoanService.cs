using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Domain.Features.Loans;

namespace Biblioteca.Application.Features.Loans
{
    public class LoanService : ILoanService
    {
        public Loan Add(Loan loan)
        {
            throw new NotImplementedException();
        }

        public void Delete(Loan loan)
        {
            throw new NotImplementedException();
        }

        public Loan Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Loan> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Loan> GetByBook(long idBook)
        {
            throw new NotImplementedException();
        }

        public Loan Update(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
