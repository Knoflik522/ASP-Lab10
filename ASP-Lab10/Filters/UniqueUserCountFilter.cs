using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Concurrent;
namespace ASP_Lab10.Filters
{
    public class UniqueUserCountFilter : IActionFilter
    {
        private static readonly ConcurrentDictionary<string, bool> UniqueUsers = new ConcurrentDictionary<string, bool>();
        private readonly string _userCountFilePath = "Logs/UserCount.txt";

        public UniqueUserCountFilter()
        {
            // Переконайтеся, що папка "Logs" існує
            Directory.CreateDirectory("Logs");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string userIp = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            // Додаємо IP до списку унікальних користувачів
            if (UniqueUsers.TryAdd(userIp, true))
            {
                // Записуємо кількість унікальних користувачів
                File.WriteAllText(_userCountFilePath, $"Unique Users Count: {UniqueUsers.Count}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Нічого не виконуємо після виконання методу
        }
    }
}
