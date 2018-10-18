using Microsoft.EntityFrameworkCore;
using Mood.Models;

namespace Mood.Data
{
    public class MoodDbContext : DbContext
    {
        public MoodDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<OdeToMood> Mood { get; set; }
    }
}
