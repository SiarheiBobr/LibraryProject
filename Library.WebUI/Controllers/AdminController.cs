using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Domain.Abstract;
using Library.Domain.Concrete;
using Library.Domain.Entities;
using Library.WebUI.Models;

namespace Library.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IBookRepository bookRepository;
        private IBookingRepository bookingRepository;
        public AdminController(IBookRepository bookRepo, IBookingRepository bookingRepo)
        {
            bookRepository = bookRepo;
            bookingRepository = bookingRepo;
        }

        //public AdminController(IBookingRepository bookingRepo)
        //{
        //    bookingRepository = bookingRepo;
        //}

        [Authorize(Roles = "admin")]
        public ViewResult List(string category = null, int page = 1)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = bookRepository.Books
                    .Where(b => category == null || b.Category == category)
                    .OrderBy(b => b.Title),
                CurrentCategory = category
            };

            return View(model);
        }


        [Authorize(Roles = "admin")]
        public ViewResult Edit(int bookId)
        {
            ViewBag.Title = "Edit book";
            Book book = bookRepository.Books
                .FirstOrDefault(p => p.BookID == bookId);
            return View(book);
        }

        [Authorize(Roles = "admin")]
        public ViewResult BookInfo(int bookId)
        {
            Book book = null;
            using (EFDbContext db = new EFDbContext())
            {
                book = db.Books.FirstOrDefault(b => b.BookID == bookId);
            }
            return View(book);
        }

        [Authorize(Roles = "admin")]
        public ViewResult Booking()
        {
            BookingsListViewModel model = new BookingsListViewModel
            {
                Bookings = bookingRepository.Bookings
                    .OrderBy(b => b.BookedDate)
                    //.Load()
                    .Include(b => b.Book)
                    .Include(b => b.User)
                    ,
                
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ViewResult Edit(Book book, HttpPostedFileBase image)
        {
            ViewBag.Title = "Edit book";
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                bookRepository.SaveBook(book);
                Book updBook = bookRepository.Books
                  .FirstOrDefault(p => p.BookID == book.BookID);
                return View(updBook);
            }
            else
            {
                return View(book);
            }
        }
        [Authorize(Roles = "admin")]
        public ViewResult Create()
        {
            ViewBag.Title = "Create new book";
            return View("Edit", new Book());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int bookid)
        {
            Book deletedProduct = bookRepository.DeleteBook(bookid);
            return RedirectToAction("List");
        }
    }
}