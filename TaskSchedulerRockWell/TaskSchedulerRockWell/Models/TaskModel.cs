namespace TaskSchedulerRockWell.Models
{
    /// <summary>
    /// Model class representing a scheduled task.
    /// </summary>
    public class TaskModel
    {
        #region Task Properties
        public CronModel Cron { get; set; }
        public string Url { get; set; }
        #endregion
    }
}
