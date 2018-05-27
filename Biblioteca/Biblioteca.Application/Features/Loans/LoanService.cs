using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Loans;

namespace Biblioteca.Application.Features.Loans
{
    public class LoanService : ILoanService
    {
        private ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public Loan Add(Loan loan)
        {
            loan.Validate();
            return _loanRepository.Save(loan);
        }

        public void Delete(Loan loan)
        {
            if (loan.Id <= 0)
                throw new IdentifierUndefinedException();
            _loanRepository.Delete(loan);
        }

        public Loan Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _loanRepository.Get(id);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _loanRepository.GetAll();
        }

        public IEnumerable<Loan> GetByBook(long idBook)
        {
            if (idBook <= 0)
                throw new IdentifierUndefinedException();
            return _loanRepository.GetByBook(idBook);
        }

        public Loan Update(Loan loan)
        {
            if (loan.Id <= 0)
                throw new IdentifierUndefinedException();
            loan.Validate();
            return _loanRepository.Update(loan);
        }
    }
}
