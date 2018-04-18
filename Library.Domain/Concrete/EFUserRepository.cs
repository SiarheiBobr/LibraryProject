using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Abstract;
using Library.Domain.Entities;


namespace Library.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

       

        public User UnblockUser(int userID)
        {
            return null;
        }

        public User BlockUser(int userID)
        {
            return null;
        }

        public User DeleteUser(int userID)
        {
            return null;
        }

    }
}
