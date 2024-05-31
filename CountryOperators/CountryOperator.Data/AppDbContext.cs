using CountryOperators.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryOperators.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryDetails> CountryDetails { get; set; }
    }
}
