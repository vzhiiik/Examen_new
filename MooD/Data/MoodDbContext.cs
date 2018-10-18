using Microsoft.EntityFrameworkCore;
using MooD.Models;

namespace MooD.Data
{
    public class MoodDbContext : DbContext
    {
        public MooDDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<OdeToMood> Mood { get; set; }
    }
}
