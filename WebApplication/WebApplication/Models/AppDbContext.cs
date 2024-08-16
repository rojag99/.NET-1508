using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employeedata> Employeedata { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employeedata>()
                .ToTable("EmployeeData") 
                .HasKey(e => e.Id);
            modelBuilder.Entity<Employeedata>()
        .Property(e => e.Name)
        .HasMaxLength(100); 

            modelBuilder.Entity<Employeedata>()
                .Property(e => e.email)
                .HasMaxLength(255);

        }
    }

}
