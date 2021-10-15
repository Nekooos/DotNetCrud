using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserCrud.Models;

namespace UserCrudTests.Mock
{
    public class MockDbContext<T> : DbSet<T> where T : class
    {
        List<T> data;

        public MockDbContext()
        {
            data = new List<T>();
        }

        public void test()
        {
            
        }
    }
}
