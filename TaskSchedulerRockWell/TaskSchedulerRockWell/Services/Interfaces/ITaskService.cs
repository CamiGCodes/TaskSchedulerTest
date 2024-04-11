using TaskSchedulerRockWell.Models;

namespace TaskSchedulerRockWell.Services.Interfaces
{
    public interface ITaskService
    {
        Task ScheduleTask(TaskModel task);
    }
}
