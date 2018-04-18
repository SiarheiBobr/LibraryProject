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
    public class BookController : Controller
    {
        private IBookRepository repository;
        //public int PageSize = 3;
        public BookController(IBookRepository productRepository)
        {
            this.repository = productRepository;
        }

        [Authorize(Roles = "user")]
        public ViewResult BookInfo(int bookId = 6)
        {
            ViewBag.IsAuth = IsAuth();
            BookInfoViewModel book = new BookInfoViewModel(repository.Books.Include(b => b.Bookings).Include(b => b.Comments).FirstOrDefault(b => b.BookID == bookId));
            return View(book);
        }

        [Authorize(Roles ="user")]
        public ViewResult List(string category, int page = 1, int pageSize = 3, string searchString = (string)null)
        {
            ViewBag.IsAuth = IsAuth();
            if (string.IsNullOrEmpty(searchString) || repository.Books.Where(b => b.Title.Contains(searchString)).Count() < 1)
            {   
                if (searchString != (string)null)
                    ViewBag.SearchMessage = "Finded 0 books";

                BooksListViewModel model = new BooksListViewModel
                {
                    Books = repository.Books
                          .Where(b => category == null || b.Category == category)
                          .OrderBy(b => b.Title)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                              repository.Books.Count() :
                              repository.Books.Where(e => e.Category == category).Count()
                    },
                    CurrentCategory = category
                };

                return View(model);
            }
            else
            {
                ViewBag.SearchMessage = "Finded " + repository.Books.Where(b => b.Title.Contains(searchString)).Count().ToString() + " books";
                BooksListViewModel model = new BooksListViewModel
                {
                    Books = repository.Books
                        .Where(b => searchString == null || b.Title.Contains(searchString))
                        .OrderBy(b => b.Title),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = 1,
                        ItemsPerPage =
                        repository.Books.Where(b => searchString == null || b.Title.Contains(searchString)).Count(),
                        TotalItems =
                            repository.Books.Where(b => searchString == null || b.Title.Contains(searchString)).Count()
                    },
                    CurrentCategory = null
                };

                return View(model);
            }
        }

        public FileContentResult GetImage(int bookId)
        {
            Book _book = repository.Books.FirstOrDefault(p => p.BookID == bookId);
            if (_book != null)
            {
                return File(_book.ImageData, _book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public string IsAuth()
        {
            string result = "вы не авторизованы";
            if(User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
            }
            return result;
        }

        //[HttpPost]
        //public PartialViewResult List(string searchString)
        //{
        //    if (string.IsNullOrEmpty(searchString))
        //        {
        //        return EmptyResult;
        //    }
        //    else
        //        if (repository.Books.Where(b => b.Title.Contains(searchString)).Count() < 1)
        //        {
        //            ViewBag.SearchMessage = "No books";
        //        return PartialView(new BooksListViewModel
        //        {
        //            Books = null,
        //            PagingInfo = new PagingInfo
        //            {
        //                CurrentPage = 1,
        //                ItemsPerPage = 1,
        //                TotalItems = 1,
        //            },
        //            CurrentCategory = null
        //        });
        //    }
        //        else
        //        {
        //            ViewBag.SearchMessage = "Finded " + repository.Books.Where(b => b.Title.Contains(searchString)).Count().ToString() + " books";
        //            BooksListViewModel model = new BooksListViewModel
        //            {
        //                Books = repository.Books
        //                    .Where(b => searchString == null || b.Title.Contains(searchString))
        //                    .OrderBy(b => b.Title),
        //                PagingInfo = new PagingInfo
        //                {
        //                    CurrentPage = 1,
        //                    ItemsPerPage =
        //                    repository.Books.Where(b => searchString == null || b.Title.Contains(searchString)).Count(),
        //                    TotalItems =
        //                        repository.Books.Where(b => searchString == null || b.Title.Contains(searchString)).Count()
        //                },
        //                CurrentCategory = null
        //            };

        //            return PartialView(model);
        //        }
        //}
    }
}