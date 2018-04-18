using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Domain.Concrete;
using Library.Domain.Entities;
using Microsoft.AspNet.SignalR;

namespace Library.WebUI.Hubs
{
    public class BookingHub : Hub
    {
        public void AddBooking(int bookId, string username)//, int bookId)
        {
            var ctx = new EFDbContext();
            var userId = ctx.Users.FirstOrDefault(u => u.Login == username).UserID;

            if (ctx.Bookings.FirstOrDefault(b => b.BookID == bookId && b.UserID == userId) != null)
            {
                Clients.All.failure();
            }
            else
            {
                var booking = new Booking { BookID = bookId, UserID = userId, Booked = true, Issued = false, BookedDate = DateTime.Now, IssuedDate = null };
                ctx.Bookings.Add(booking);
                ctx.SaveChanges();
                Clients.All.receivedNewBooking(booking.BookID, username, booking.BookedDate);
            }
        }

        public void RemoveBooking(int bookId, string username)//, int bookId)
        {
            var ctx = new EFDbContext();
            var userId = ctx.Users.FirstOrDefault(u => u.Login == username).UserID;
            var booking = ctx.Bookings.FirstOrDefault(b => b.BookID == bookId && b.UserID == userId);

            if (booking != null)
            {
                ctx.Bookings.Remove(booking);
                ctx.SaveChanges();

                //Clients.All.receivedNewComment( username, message);
                Clients.All.receivedRemovedBooking(booking.BookID, username, booking.BookedDate);
            }
            else
            {
                Clients.All.failure();
            }



        }
    }
}