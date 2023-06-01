using Microsoft.Extensions.Logging;

namespace DogsApp_Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogMessageAndThrowException(this ILogger logger, string message, Exception exception)
        {
            logger?.LogError(exception, message);
            throw exception;
        }
    }
}
