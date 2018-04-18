using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Abstract;
using Library.Domain.Entities;

namespace Library.Domain.Concrete
{
    public class EFCommentRepository : ICommentRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Comment> Comments
        {
            get { return context.Comments; }
        }
        public void DeleteComment(int commentID)
        {
            Comment comment = context.Comments.Find(commentID);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
        }
        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }
    }
}
