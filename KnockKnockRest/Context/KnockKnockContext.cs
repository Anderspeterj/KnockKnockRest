using Microsoft.EntityFrameworkCore;
using KnockKnockRest.Models;
namespace KnockKnockRest.Context
{
    public class KnockKnockContext : DbContext
    {
        public KnockKnockContext(DbContextOptions<KnockKnockContext> options) : base(options) { }

        public DbSet<Arrival> arrivals { get; set; }
        public DbSet<Student> students { get; set; }
    }
}
