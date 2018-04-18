using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Abstract;
using Library.Domain.Entities;

namespace Library.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Book> Books
        {
            get { return context.Books; }
        }

        public void SaveBook(Book book)
        {

            if (book.BookID == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookID);
                if (dbEntry != null)
                {
                    dbEntry.Author = book.Author;
                    dbEntry.Description = book.Description;
                    dbEntry.Year = book.Year;
                    dbEntry.Category = book.Category;
                    dbEntry.Title = book.Title;
                    dbEntry.Count = book.Count;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Book DeleteBook(int bookID)
        {
            Book dbEntry = context.Books.Find(bookID);
            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Book BookedCount(int bookID)
        {
            Book dbEntry = context.Books.Find(bookID);
            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
