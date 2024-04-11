namespace TaskSchedulerRockWell.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public CronModel Cron { get; set; }
        public string Url { get; set; }
    }
}
