using Biblioteca.Domain.Features.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Features.Loans
{
    public class LoanRepository : ILoanRepository
    {
        public void Delete(Loan book)
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

        public Loan Save(Loan book)
        {
            throw new NotImplementedException();
        }

        public Loan Update(Loan book)
        {
            throw new NotImplementedException();
        }
    }
}
