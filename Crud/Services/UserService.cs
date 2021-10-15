using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UserCrud.Models;
using System;

namespace UserCrud.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public User Delete(User user)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userContext.Users.ToList();
        }

        public User GetById(long id)
        {
            return _userContext.Users.Find(id);
            
        }

        public void Put(User user, long id)
        {
            _userContext.Entry(user).State = EntityState.Modified;
            _userContext.SaveChanges();
        }

        public User Save(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            return user;
        }

        public bool UserExists(long id)
        {
            return _userContext.Users.Any(user => user.Id == id);
        }
    }
}
