using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Employeedata> Employeedata { get; set; }

    }
}
