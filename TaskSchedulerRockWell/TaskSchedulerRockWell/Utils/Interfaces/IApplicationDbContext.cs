using Microsoft.EntityFrameworkCore;
using TaskSchedulerRockWell.Models;

namespace TaskSchedulerRockWell.Utils.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<HangFireState> HangFireState { get; set; }
    }
}
