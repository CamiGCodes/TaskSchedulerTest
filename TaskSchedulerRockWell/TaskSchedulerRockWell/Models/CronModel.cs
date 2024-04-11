namespace TaskSchedulerRockWell.Models
{
    public class CronModel
    {
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string DayOfMonth { get; set; }
        public string Month { get; set; }
        public string DayOfWeek { get; set; }
    }

}
