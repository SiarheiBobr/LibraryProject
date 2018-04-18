using Library.Domain.Concrete;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Library.WebUI.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            List<string> roles = new List<string>();
            using (EFDbContext db = new EFDbContext())
            {
                Role[] userRoles = db.Roles.ToArray();
                foreach (var role in userRoles)
                {
                    roles.Add(role.Name);
                } 
            }
            return roles.ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (EFDbContext db = new EFDbContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == username || u.Login == username);
                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleID);
                    if (userRole != null)
                        roles = new string[] { userRole.Name };
                }
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            List<string> usersResult = new List<string>();
            using (EFDbContext db = new EFDbContext())
            {
                Role role = db.Roles.FirstOrDefault(r => r.Name == roleName);
                User[] users = db.Users.Where(u=> u.RoleID == role.RoleID).ToArray();
                foreach (var user in users)
                {
                    usersResult.Add(user.Login);
                }
                
            }
            return usersResult.ToArray();
        } 

        public override bool IsUserInRole(string username, string roleName)
        {
            bool result = false;

            using (EFDbContext db = new EFDbContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == username || u.Login == username);
                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleID);
                    if (userRole != null && userRole.Name == roleName)
                        result = true;
                }
            }

            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}