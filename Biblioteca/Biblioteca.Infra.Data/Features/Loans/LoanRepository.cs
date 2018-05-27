using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Books;
using Biblioteca.Domain.Features.Loans;
using DonaLaura.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Features.Loans
{
    public class LoanRepository : ILoanRepository
    {

        #region#Scripts

        private readonly string _insert = "INSERT INTO TBLoan (NameClient, DateDevolution, BookId) VALUES (@NameClient,@DateDevolution,@BookId)";

        private const string _getById = "SELECT l.*, b.Title AS book_Title, b.Theme AS book_Theme, b.Author AS book_Author, b.Volume AS book_Volume, b.DatePost AS book_DatePost, b.IsAvaliable AS book_IsAvaliable FROM TBLoan AS l INNER JOIN TBBook AS b ON (l.BookId = b.IdBook) WHERE l.IdLoan = @IdaLoan";

        private const string _update = "UPDATE TBLoan SET NameClient = @NameClient, " +
                                                           "DateDevolution = @DateDevolution," +
                                                           "BookId = @BookId," +
                                                           " WHERE IdLoan = @IdLoan";

        private const string _getAll = "SELECT * FROM TBLoan";

        private const string _getAllTeste = "SELECT l.*, b.Title AS book_Title, b.Theme AS book_Theme, b.Author AS book_Author, b.Volume AS book_Volume, b.DatePost AS book_DatePost, b.IsAvaliable AS book_IsAvaliable FROM TBLoan AS l INNER JOIN TBBook AS b ON (l.BookId = b.IdBook)";

        private const string _delete = "DELETE FROM TBLoan WHERE IdLoan = @IdLoan";

        private const string _getByBook = "SELECT l.*, b.Title AS book_Title, b.Theme AS book_Theme, b.Author AS book_Author, b.Volume AS book_Volume, b.DatePost AS book_DatePost, b.IsAvaliable AS book_IsAvaliable FROM TBLoan AS l INNER JOIN TBBook AS b ON (l.BookId = b.IdBook) WHERE l.BookId = @IdBook";

        #endregion#Scripts

        public void Delete(Loan book)
        {
            if (book.Id <= 0)
                throw new IdentifierUndefinedException();
            Db.Delete(_delete, new object[] { "@IdOrder", book.Id });
        }

        public Loan Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_getById, Make, new object[] { "@IdLoan ", id });
        }

        public IEnumerable<Loan> GetAll()
        {
            return Db.GetAll(_getAllTeste, Make);
        }

        public IEnumerable<Loan> GetByBook(long idBook)
        {
            if (idBook <= 0)
                throw new IdentifierUndefinedException();
            return Db.GetAll(_getByBook, Make, new object[] { "@IdBook ", idBook });
        }

        public Loan Save(Loan book)
        {
            book.Validate();
            book.Id = Db.Insert(_insert, Take(book));

            return book;
        }

        public Loan Update(Loan book)
        {
            if (book.Id <= 0)
                throw new IdentifierUndefinedException();

            book.Validate();

            Db.Update(_update, Take(book));

            return book;
        }

        private static Func<IDataReader, Loan> Make = reader =>
           new Loan
           {
               Id = Convert.ToInt64(reader["IdLoan"]),
               ClientName = Convert.ToString(reader["NameClient"]),
               DateDevolution = Convert.ToDateTime(reader["DateDevolution"]),
               Book = new Book()
               {
                   Id = Convert.ToInt32(reader["BookId"]),
                   Title = Convert.ToString(reader["book_Title"]),
                   Theme = Convert.ToString(reader["book_Theme"]),
                   Author = Convert.ToString(reader["book_Author"]),
                   Volume = Convert.ToInt32(reader["book_Volume"]),
                   PostDate = Convert.ToDateTime(reader["book_DatePost"]),
                   IsAvaliable = Convert.ToBoolean(reader["book_IsAvaliable"])
               }

           };

        private object[] Take(Loan loan)
        {
            return new object[]
            {
                "@IdLoan", loan.Id,
                "@NameClient", loan.ClientName,
                "@Datedevolution", loan.DateDevolution,
                "@IdBook", loan.Book.Id
            };
        }
    }
}
