﻿using Biblioteca.Domain.Common;
using Biblioteca.Domain.Features.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Loans
{
    public class Loan : Entity
    {
        public string ClientName { get; set; }
        public DateTime DateDevolution { get; set; }
        public Book Book { get; set; }
        public double Fine { get; set; }

        public override void Validate()
        {
            if (String.IsNullOrEmpty(ClientName))
                throw new LoanClientNameNullOrEmptyException();
            if (ClientName.Length >= 100)
                throw new LoanClientNameOverFlowException();
            if (DateDevolution <= DateTime.Now)
                throw new LoanInvalidDateDevolutionException();
            if (Book == null)
                throw new LoanNoBookException();
            if (!Book.IsAvaliable)
                throw new LoanWithBookNotAvaliableException();
        }

        public void CalculateFine()
        {
            if(DateDevolution < DateTime.Now)
            {
                TimeSpan intervalo = DateTime.Now - DateDevolution;
                Fine = intervalo.Days * 2.5;
            }
        }
    }
}
