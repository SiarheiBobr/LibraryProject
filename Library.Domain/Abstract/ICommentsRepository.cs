using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Domain.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }

        void DeleteComment(int bookingID);

        void AddComment(Comment booking);

    }
}
