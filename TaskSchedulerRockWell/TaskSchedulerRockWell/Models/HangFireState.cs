namespace TaskSchedulerRockWell.Models
{
    public class HangFireState
    {
        public long Id { get; set; } 
        public long JobId { get; set; }  
        public string Name { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }  
        public string? Data { get; set; }
    }
}
