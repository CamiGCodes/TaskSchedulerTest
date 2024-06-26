﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services;
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
        private readonly IHangFireStateService _hangFireStateService;

        /// <summary>
        /// Constructor for TaskController. Injects the task service dependency.
        /// </summary>
        /// <param name="taskService">The injected task service.</param>
        public TaskController(ITaskService taskService, IHangFireStateService hangFireStateService)
        {
            _taskService = taskService;
            _hangFireStateService = hangFireStateService;
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
                return Ok(new { message = "Task scheduled successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex} An error occurred while processing the request.");
            }
        }

        [HttpGet("getHangFireState")]
        public async Task<ActionResult<IEnumerable<HangFireState>>> GetHangfireState()
        {
            var hangfireStates = await _hangFireStateService.GetHangfireStatesAsync();

            var serializedStates = hangfireStates.Select(state => new HangFireState
            {
                Id = state.Id,
                JobId = state.JobId,
                Name = state.Name,
                Reason = state.Reason,
                CreatedAt = state.CreatedAt,
                Data = state.Data ?? "" 
            });
            return Ok(hangfireStates);
        }
        #endregion
    }
}
