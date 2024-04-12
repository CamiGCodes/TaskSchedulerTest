using Microsoft.EntityFrameworkCore;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;
using TaskSchedulerRockWell.Utils;

namespace TaskSchedulerRockWell.Services
{
    public class HangFireStateService : IHangFireStateService
    {
        private readonly ApplicationDbContext _context;

        public HangFireStateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<HangFireState>> GetHangfireStatesAsync()
        {
            var query = _context.HangFireState.FromSqlRaw("SELECT TOP (1000) [Id], [JobId], [Name], [Reason], [CreatedAt], COALESCE([Data], '') AS [Data] FROM [HangfireTest].[HangFire].[State]");
            return query.AsQueryable();
        }
    }
}
