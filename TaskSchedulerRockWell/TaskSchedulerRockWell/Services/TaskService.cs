using Hangfire;
using System.Net.NetworkInformation;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services.Interfaces;

namespace TaskSchedulerRockWell.Services
{
    /// <summary>
    /// Service class responsible for scheduling and executing tasks.
    /// </summary>
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
        #region Cron Expression Generation

        /// <summary>
        /// Generates a cron expression string based on the provided CronModel object.
        /// </summary>
        /// <param name="cron">The CronModel object containing scheduling information.</param>
        /// <returns>A string representing the cron expression.</returns>
        private string GenerateCronExpression(CronModel cron)
        {
            return $"{cron.Minutes} {cron.Hours} {cron.DayOfMonth} {cron.Month} {cron.DayOfWeek}";
        }
        #endregion

        #region Task Scheduling

        /// <summary>
        /// Schedules a task to scrape headers from a specified URL at a recurring interval.
        /// </summary>
        /// <param name="task">The TaskModel object containing the URL and scheduling details.</param>
        /// <returns>An asynchronous Task representing the scheduling operation.</returns>
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
        #endregion

        #region Task Execution
        /// <summary>
        /// Executes the scheduled task to scrape headers from the provided URL.
        /// </summary>
        /// <param name="url">The URL of the website to scrape headers from.</param>
        /// <returns>An asynchronous Task that returns a dictionary of scraped headers, or throws an exception if unsuccessful.</returns>
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
        #endregion

        #region Helper Methods
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
        #endregion
    }
}
