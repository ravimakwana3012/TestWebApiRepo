using Microsoft.EntityFrameworkCore;

namespace TestWebApi_DAL.Models
{
    public class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        {

        }
        public DbSet<book> book { get; set; }
    }
}
