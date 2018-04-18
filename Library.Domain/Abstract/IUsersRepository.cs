using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        //void Register(User user);

        User DeleteUser(int userID);

        User BlockUser(int userID);

        User UnblockUser(int userID);


    }
}
