using Microsoft.EntityFrameworkCore;


namespace MyEntityAssignment.Models
{
    public class StaffDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }


        public StaffDbContext(DbContextOptions<StaffDbContext> options)
            : base(options)
        {
        }

        public StaffDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-358RPE4\SQLEXPRESS; 
                                        Initial Catalog = Staff; Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leave>()
                .Property(c => c.LeaveType)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
