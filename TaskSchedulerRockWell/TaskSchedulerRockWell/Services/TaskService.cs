using Hangfire;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;

namespace TaskSchedulerRockWell.Services
{
    //TODO: Summaries
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private Dictionary<string, string> _headers;

        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }
        
        private string GenerateCronExpression(CronModel cron)
        {
            return $"{cron.Minutes} {cron.Hours} {cron.DayOfMonth} {cron.Month} {cron.DayOfWeek}";
        }

        public async Task<Dictionary<string, string>> ScheduleTask(TaskModel task)
        {
            try
            {
                string cronExpression = GenerateCronExpression(task.Cron);

                ValidateUrl(task.Url);

                PingReply pingReply = await PingUrl(task.Url);

                Uri validUri = ParseUrl(task.Url);

                return await ScrapeHeaders(validUri);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred: {ex.Message}");
                throw; // Rethrow the original exception
            }
        }

        private void ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("The URL cannot be empty.");
                throw new ArgumentException("The URL cannot be empty.");
            }
        }

        private async Task<PingReply> PingUrl(string url)
        {
            Ping ping = new Ping();
            return await ping.SendPingAsync(url);
        }

        private Uri ParseUrl(string url)
        {
            if (Uri.TryCreate($"https://{url}", UriKind.Absolute, out Uri uri))
            {
                _logger.LogInformation($"Valid URI format: {uri}");
                return uri;
            }
            else
            {
                _logger.LogError($"Invalid URI format: {url}");
                throw new ArgumentException("Invalid URI format. Please provide a valid URL.");
            }
        }

        private async Task<Dictionary<string, string>> ScrapeHeaders(Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                Dictionary<string, string> headers = new Dictionary<string, string>();

                // Iterate over the response headers and add them to the dictionary
                foreach (var header in response.Headers)
                {
                    headers.Add(header.Key, string.Join(",", header.Value));
                }

                return headers;
            }
        }
    }
}
