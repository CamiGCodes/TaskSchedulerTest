namespace TaskSchedulerRockWell.Utils
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Entity Framework Core DbContext class for the application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
