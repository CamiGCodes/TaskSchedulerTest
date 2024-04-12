using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.NetworkInformation;
using TaskSchedulerRockWell.Models;
using TaskSchedulerRockWell.Services;

public class TaskServiceTests
{
    private readonly Mock<ILogger<TaskService>> _mockLogger;
    private readonly Mock<IRecurringJobManager> _mockRecurringJobManager;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mockLogger = new Mock<ILogger<TaskService>>();
        _mockRecurringJobManager = new Mock<IRecurringJobManager>();
        _taskService = new TaskService(_mockLogger.Object, _mockRecurringJobManager.Object);
    }

    [Fact]
    public void ScheduleTask_ValidTask_ShouldNotBeNull()
    {
        // Arrange
        var task = new TaskModel { Url = "https://example.com", Cron = new CronModel { Minutes = "*", Hours = "*", DayOfMonth = "*", Month = "*", DayOfWeek = "*" } };

        // Act
        var result = _taskService.ScheduleTask(task);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void ScheduleTask_ValidTask_ShouldScheduleSuccessfully()
    {
        // Arrange
        var task = new TaskModel { Url = "https://example.com", Cron = new CronModel { Minutes = "*", Hours = "*", DayOfMonth = "*", Month = "*", DayOfWeek = "*" } };
        var cronExpression = "*/5 * * * *"; // Example cron expression
        var options = new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc }; // Example of setting options
        Job job = Job.FromExpression(() => ExecuteScheduledTaskLogic(task.Url));

        // Act
        _taskService.ScheduleTask(task);

        // Assert
        _mockRecurringJobManager.Verify(
            client => client.AddOrUpdate(
                "ScrapeHeaders",
                job,
                cronExpression,
                options),
            Times.Once);
    }

    public async Task<Dictionary<string, string>> ExecuteScheduledTaskLogic(string url)
    {
        // Simular la validación de URL
        if (string.IsNullOrEmpty(url))
        {
            // Simular un error si la URL es nula o vacía
            throw new ArgumentException("The URL cannot be empty.");
        }

        // Simular un resultado exitoso del ping
        var pingSuccessful = true;

        if (!pingSuccessful)
        {
            // Simular un error si el ping no es exitoso
            throw new PingException("Ping failed.");
        }

        // Simular el análisis de la URL
        var validUri = new Uri(url);

        // Simular el raspado de encabezados
        var headers = new Dictionary<string, string>
    {
        { "Content-Type", "text/html" },
        { "Server", "Apache" },
        // Agregar más encabezados si es necesario para las pruebas
    };

        return headers;
    }
}

