using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Abstract;
using Library.Domain.Entities;

namespace Library.Domain.Concrete
{
    public class EFBookingRepository : IBookingRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Booking> Bookings
        {
            get { return context.Bookings; }
        }

        public void SaveBooking(Booking booking)
        {

           
            
                Booking dbEntry = context.Bookings.Find(booking.BookingID);
                if (dbEntry != null)
                {
                    dbEntry.UserID = booking.UserID;
                    dbEntry.BookID = booking.BookID;
                    dbEntry.Booked = booking.Booked;
                    dbEntry.BookedDate = booking.BookedDate;
                    dbEntry.Issued = booking.Issued;
                    dbEntry.IssuedDate = booking.IssuedDate;
                }
            
            context.SaveChanges();
        }

        public void DeleteBooking(int bookingID)
        {
            Booking booking = context.Bookings.Find(bookingID);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                context.SaveChanges();
            }
        }

        public void AddBooking(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public void HandOut(int bookingID)
        {
            Booking booking = context.Bookings.FirstOrDefault(b => b.BookingID == bookingID);
            if (booking != null)
            {
                booking.Issued = true;
                booking.IssuedDate = DateTime.Now;
            }

            context.SaveChanges();
        }
    }
}
