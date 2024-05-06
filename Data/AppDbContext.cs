using SecurityCasus.Models;
using Microsoft.EntityFrameworkCore;



namespace SecurityCasus.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<MyData> Test { get; set; }
    }
}
