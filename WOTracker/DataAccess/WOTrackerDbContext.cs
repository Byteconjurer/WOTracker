using WOTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace WOTracker.DataAccess
{
    internal class WOTrackerDbContext : DbContext
    {
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<Exercises> Exercises { get; set; }

     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDirectory, "WOTracker.db");
            string connectionString = $"Data Source={dbPath}";

            Console.WriteLine($"Database path: {dbPath}");
            optionsBuilder.UseSqlite(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutDay>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.Days);

            modelBuilder.Entity<Exercises>().HasKey(e => e.Name);
        }
    }
    
}
