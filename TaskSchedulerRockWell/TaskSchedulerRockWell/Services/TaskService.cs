using Hangfire;
using System.Net.NetworkInformation;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;

namespace TaskSchedulerRockWell.Services
{
    //TODO: Summaries
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private Dictionary<string, string> _headers;
        private IRecurringJobManager _client;
        public TaskService(ILogger<TaskService> logger, IRecurringJobManager client)
        {
            _logger = logger;
            _client = client;
        }

        private string GenerateCronExpression(CronModel cron)
        {
            return $"{cron.Minutes} {cron.Hours} {cron.DayOfMonth} {cron.Month} {cron.DayOfWeek}";
        }

        public async Task ScheduleTask(TaskModel task)
        {
            try
            {
                string cronExpression = GenerateCronExpression(task.Cron);

                _client.AddOrUpdate(
                $"ScrapeHeaders_{task.Id}",
                () => ExecuteScheduledTask(task.Url),
                cronExpression);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, string>> ExecuteScheduledTask(string url)
        {
            try
            {
                ValidateUrl(url);

                PingReply pingReply = await PingUrl(url);

                Uri validUri = ParseUrl(url);

                _headers = await ScrapeHeaders(validUri);

                return _headers;
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
