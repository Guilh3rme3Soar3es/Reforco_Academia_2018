using Biblioteca.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Features.Books
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Author { get; set; }
        public int Volume { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsAvaliable { get; set; }

        public override void Validate()
        {
            if (String.IsNullOrEmpty(Title))
            {
                throw new BookTitleNullOrEmptyException();
            }
            else if (Title.Length < 4)
            {
                throw new BookShortTitleException();
            }
            else if (Title.Length < 100)
            {
                throw new BookTitleOverFlowException();
            }
            if (String.IsNullOrEmpty(Theme))
            {
                throw new BookThemeNullOremptyException();
            }
            else if(Theme.Length < 4)
            {
                throw new BookShortThemeException();
            }
            else if (Theme.Length > 100)
            {
                throw new BookThemeOverFlowException();
            }
            if (Volume <= 0)
                throw new BookInvalidVolumeException();
            if (PostDate > DateTime.Now)
                throw new BookInvalidPostDateException();
        }
    }
}
