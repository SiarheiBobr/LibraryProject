using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Domain.Entities;

namespace Library.WebUI.Models
{
    public class BookInfoViewModel: Book
    {
       
        public int FreeCount { get; set; }
        public Boolean FreeToBooking { get; set; }

        public BookInfoViewModel() : base() { }
        public BookInfoViewModel(Book book) 
        {
            BookID = book.BookID;
            Bookings = book.Bookings;
            Category = book.Category;
            Comments = book.Comments;
            Count = book.Count;
            Description = book.Description;
            ImageData = book.ImageData;
            ImageMimeType = book.ImageMimeType;
            Title = book.Title;
            Year = book.Year;
            FreeCount = Count - Bookings.Count;
            FreeToBooking = (FreeCount > 0) ? true : false;
        }
    }
}