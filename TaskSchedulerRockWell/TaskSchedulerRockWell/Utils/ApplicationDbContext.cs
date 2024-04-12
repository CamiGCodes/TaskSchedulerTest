using Microsoft.EntityFrameworkCore;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Utils.Interfaces;

namespace TaskSchedulerRockWell.Utils
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<HangFireState> HangFireState { get; set; } // DbSet para HangFireState

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
