namespace TaskSchedulerRockWell.Models
{
    /// <summary>
    /// Model class representing a cron expression for scheduling tasks.
    /// </summary>
    public class CronModel
    {
        #region Properties
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string DayOfMonth { get; set; }
        public string Month { get; set; }
        public string DayOfWeek { get; set; }
        #endregion
    }
}
