using Library.Domain.Concrete;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.WebUI.Controllers
{
    public class BookingsController : ApiController
    {
        private EFDbContext _ctx;
        public BookingsController()
        {


            this._ctx = new EFDbContext();
        }

        // GET api/<controller>
        public IEnumerable<Booking> Get(int bookId)
        {
            var a = this._ctx.Bookings.Where(c => c.BookID == bookId && c.Booked == true).OrderByDescending(keySelector: x => x.BookedDate).Include(c => c.User).Include(c => c.Book).ToList();
            return a;
        }
    }
}
