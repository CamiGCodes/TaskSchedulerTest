using Microsoft.EntityFrameworkCore;
using TaskSchedulerRockWell.Models;

namespace TaskSchedulerRockWell.Utils
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<HangFireState> HangFireState { get; set; } // DbSet para HangFireState

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
