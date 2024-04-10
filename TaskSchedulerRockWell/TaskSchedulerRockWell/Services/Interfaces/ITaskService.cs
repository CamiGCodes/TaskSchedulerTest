using TaskSchedulerRockWell.Models;

namespace TaskSchedulerRockWell.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Dictionary<string, string>> ScheduleTask(TaskModel task);
    }
}
