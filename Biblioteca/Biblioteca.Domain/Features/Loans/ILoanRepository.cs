using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Loans
{
    public interface ILoanRepository
    {
        Loan Save(Loan book);
        Loan Update(Loan book);
        Loan Get(long id);
        IEnumerable<Loan> GetAll();
        void Delete(Loan book);

        IEnumerable<Loan> GetByBook(long idBook);
    }
}
