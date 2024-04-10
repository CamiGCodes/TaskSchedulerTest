using System.Net.NetworkInformation;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;

namespace TaskSchedulerRockWell.Services
{
    //TODO: Summaries
    //TODO: Validar URI válida y cron válido.
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;

        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }

        public async Task<Dictionary<string, string>> ScheduleTask(TaskModel task)
        {
            try
            {
                //Ping website
                Ping ping = new Ping();
                PingReply reply = await ping.SendPingAsync(task.Url);

                if (reply.Status == IPStatus.Success)
                {
                    string absoluteUrl = $"https://{task.Url}";

                    _logger.LogInformation($"Ping to {absoluteUrl} succesful. Response time: {reply.RoundtripTime} ms");

                    
                    //Scraping headers
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(absoluteUrl);
                        response.EnsureSuccessStatusCode();

                        Dictionary<string, string> headers = new Dictionary<string, string>();

                        foreach (var header in response.Headers)
                        {
                            headers.Add(header.Key, string.Join(",", header.Value));
                        }

                        return headers;
                    }
                }
                else
                {
                    Console.WriteLine($"Ping to {task.Url} has failed. Status: {reply.Status}");
                    return new Dictionary<string, string>();

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error executing task: {ex.Message}");
                return null;
                
            }

        }
    }
}
