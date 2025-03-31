using Microsoft.EntityFrameworkCore;
using EventManagementAPI.Models;
using Microsoft.Extensions.Logging;

namespace EventManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
