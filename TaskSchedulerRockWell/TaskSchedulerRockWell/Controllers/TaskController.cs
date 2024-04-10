using Microsoft.AspNetCore.Mvc;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;

namespace TaskSchedulerRockWell.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("scheduleTask")]
        public async Task<IActionResult> ScheduleTask([FromBody] TaskModel task)
        {
            try
            {
                Dictionary<string, string> headers = await _taskService.ScheduleTask(task);
                return Ok(headers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex} An error occurred while processing the request.");
            }
        }
    }
}
