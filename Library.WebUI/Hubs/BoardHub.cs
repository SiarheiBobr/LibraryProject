using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Domain.Concrete;
using Library.Domain.Entities;
using Microsoft.AspNet.SignalR;

namespace Library.WebUI.Hubs
{
    public class BoardHub : Hub
    {
        public void WriteComment(int bookId, string username, string message)//, int bookId)
        {
            var ctx = new EFDbContext();
            var userId = ctx.Users.FirstOrDefault(u => u.Login == username).UserID;
            var comment = new Comment{ Text = message, UserID = userId, BookID = bookId, CreationDate = DateTime.Now };
            ctx.Comments.Add(comment);
            ctx.SaveChanges();

            //Clients.All.receivedNewComment( username, message);
            Clients.All.receivedNewComment(comment.CommentID, comment.User.FirstName + " " + comment.User.LastName, comment.Text, comment.CreationDate);
        }
    }
}