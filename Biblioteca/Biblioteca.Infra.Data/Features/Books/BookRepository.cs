using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Features.Books;
using DonaLaura.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Features.Books
{
    public class BookRepository : IBookRepository
    {

        #region#Scripts
        private const string _insert = "INSERT INTO TBBook (Title, Theme, Author, Volume, PostDate, IsAvaliable) " +
                                    "VALUES (@Title, @Theme, @Author, @Volume, @PostDate, @IsAvaliable)";

        private const string _getById = "SELECT * FROM TBBook WHERE IdBook = @IdBook";

        private const string _update = "UPDATE TBBook SET Title = @Title, " +
                                                            "Theme = @Theme, " +
                                                            "Author = @Author, " +
                                                            "Volume = @Volume, " +
                                                            "PostDate = @PostDate, " +
                                                            "IsAvaliable = @IsAvaliable WHERE IdBook = @IdBook";

        private const string _getAll = "SELECT * FROM TBBook";

        private const string _delete = "DELETE FROM TBBook WHERE IdBook = @IdBook";

        #endregion#Scripts

        public void Delete(Book book)
        {
            if (book.Id < 0)
                throw new IdentifierUndefinedException();
            Db.Delete(_delete, new object[] { "@IdBook", book.Id });
        }

        public Book Get(long id)
        {
            if (id < 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_getById, Make, new object[] { "@IdBook ", id });

        }

        public IEnumerable<Book> GetAll()
        {
            return Db.GetAll(_getAll, Make);
        }

        public Book Save(Book book)
        {
            book.Validate();
            book.Id = Db.Insert(_insert, Take(book));

            return book;
        }

        public Book Update(Book book)
        {
            if (book.Id < 0)
                throw new IdentifierUndefinedException();
            book.Validate();

            Db.Update(_update, Take(book));

            return book;
        }

        private static Func<IDataReader, Book> Make = reader =>
           new Book
           {
               Id = Convert.ToInt64(reader["IdBook"]),
               Title = reader["Author"].ToString(),
               Author = reader["Author"].ToString(),
               Theme = reader["Theme"].ToString(),
               Volume = Convert.ToInt32(reader["Volume"]),
               PostDate = Convert.ToDateTime(reader["PostDate"]),
               IsAvaliable = Convert.ToBoolean(reader["IsAvaliable"])

           };

        private object[] Take(Book book)
        {
            return new object[]
            {
                "@IdBook", book.Id,
                "@Title", book.Title,
                "@Theme", book.Theme,
                "@Author", book.Author,
                "@Volume", book.Volume,
                "@PostDate", book.PostDate,
                "@IsAvaliable" , book.IsAvaliable

            };
        }
    }
}
