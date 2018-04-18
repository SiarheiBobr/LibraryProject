using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Domain.Abstract
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }

        void SaveBook(Book book);

        Book DeleteBook(int bookID);

    }
}
