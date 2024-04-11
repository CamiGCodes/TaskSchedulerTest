using TaskSchedulerRockWell.Models;

namespace TaskSchedulerRockWell.Services.Interfaces
{
    /// <summary>
    /// Interface defining methods for scheduling tasks.
    /// </summary>
    public interface ITaskService
    {
        #region Task Scheduling

        /// <summary>
        /// Schedules a new task based on the provided TaskModel object.
        /// </summary>
        /// <param name="task">The TaskModel object representing the task to be scheduled.</param>
        /// <returns>A Task representing the asynchronous operation of scheduling the task.</returns>
        Task ScheduleTask(TaskModel task);
        #endregion
    }
}
