using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Domain.Abstract
{
    public interface IBookingRepository
    {
        IQueryable<Booking> Bookings { get; }

        void SaveBooking(Booking booking);

        void DeleteBooking(int bookingID);

        void AddBooking(Booking booking);

        void HandOut(int bookingID);

    }
}
