using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Domain.Entities;

namespace Library.WebUI.Models
{
    public class BookingsListViewModel
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}