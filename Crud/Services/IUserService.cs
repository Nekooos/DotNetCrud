using System.Collections.Generic;
using System.Threading.Tasks;
using UserCrud.Models;

namespace UserCrud.Services
{
    public interface IUserService
    {
        User GetById(long id);

        IEnumerable<User> GetAll();

        User Save(User user);

        void Put(User user, long id);

        User Delete(User user);

        bool UserExists(long id);
    }
}
