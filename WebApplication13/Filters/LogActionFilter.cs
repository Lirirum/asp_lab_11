using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication13.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly string logFilePath;

        public LogActionFilter(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string userId = context.HttpContext.Connection.Id.ToString();

            string logMessage = $"{timestamp}: Action '{actionName}' started by userd {userId}.";
            try
            {
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
