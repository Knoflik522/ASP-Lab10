using Microsoft.AspNetCore.Mvc.Filters;
namespace ASP_Lab10.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly string _logFilePath = "Logs/ActionLogs.txt";

        public LogActionFilter()
        {
            // Переконайтеся, що папка "Logs" існує
            Directory.CreateDirectory("Logs");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string methodName = context.ActionDescriptor.DisplayName;
            string logMessage = $"Method: {methodName}, Time: {DateTime.Now}\n";

            File.AppendAllText(_logFilePath, logMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
