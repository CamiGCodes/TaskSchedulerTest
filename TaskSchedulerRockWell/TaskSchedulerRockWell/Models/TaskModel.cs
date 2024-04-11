using Hangfire;

namespace TaskSchedulerRockWell.Models
{
    public class TaskModel
    {
        public CronModel Cron { get; set; }
        public string Url { get; set; }
    }
}
