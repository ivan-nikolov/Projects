namespace Projects.Data
{
    using Microsoft.EntityFrameworkCore;
    using Projects.Data.Models;

    /// <summary>
    /// Application DB Context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context Options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TimeLog> TimeLogs { get; set; }
    }
}
