using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProgettoFinale.Models;
using System.Configuration;

namespace ProgettoFinale.Persistence.Configuration
{
    public class DatabaseCxt : DbContext
    {
        public readonly string _connectionString;

        public DatabaseCxt(DbContextOptions<DatabaseCxt> opts, IOptions<AppSettings> setting) : base(opts)
        {
            _connectionString = setting.Value.ConnectionString;
        }
        public DatabaseCxt()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                     .HasMany(e => e.Cities)
                     .WithOne(e => e.Country)
                     .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Continent> Continent { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
    }
}
