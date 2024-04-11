using Microsoft.AspNetCore.Mvc;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;

namespace TaskSchedulerRockWell.Controllers
{
    /// <summary>
    /// Controller class for handling task scheduling operations.
    /// </summary>
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Constructor for TaskController. Injects the task service dependency.
        /// </summary>
        /// <param name="taskService">The injected task service.</param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        #region Schedule Task Endpoint

        /// <summary>
        /// Schedules a new task based on the provided TaskModel object.
        /// </summary>
        /// <param name="task">The TaskModel object representing the task to be scheduled.</param>
        /// <returns>An IActionResult indicating the outcome of the scheduling operation.</returns>
        [HttpPost("scheduleTask")]
        public async Task<IActionResult> ScheduleTask([FromBody] TaskModel task)
        {
            try
            {
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                await _taskService.ScheduleTask(task);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex} An error occurred while processing the request.");
            }
        }
        #endregion
    }
}
