using TaskSchedulerRockWell.Models;

namespace TaskSchedulerRockWell.Services.Interfaces
{
    public interface IHangFireStateService
    {
        Task<IQueryable<HangFireState>> GetHangfireStatesAsync();
    }
}
