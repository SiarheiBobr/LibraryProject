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
    public class CommentsController : ApiController
    {
        private EFDbContext _ctx;
        public CommentsController()
        {


            this._ctx = new EFDbContext();
        }

        // GET api/<controller>
        public IEnumerable<Comment> Get(int bookId)
        {
            var a = this._ctx.Comments.Where(c => c.BookID == bookId).OrderByDescending(keySelector: x => x.CreationDate).Include(c => c.User).ToList();
            return a;
        }
    }
}
