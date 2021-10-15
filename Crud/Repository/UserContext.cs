using Microsoft.EntityFrameworkCore;

namespace UserCrud.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {

        }

        public UserContext()
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
