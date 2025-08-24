using System;
using Microsoft.Extensions.Logging;


namespace MvcMovie.Helpers
{
    public static class LoggerHelper
    {
        public static void Info(this ILogger logger, string? message, params object[] args) => logger.LogInformation("{Time}: " + message, DateTimeOffset.UtcNow, args);
            public static void Warn(this ILogger logger, string? message, params object[] args) => logger.LogWarning("{Time}: " + message, DateTimeOffset.UtcNow, args);
            public static void Error(this ILogger logger, string? message, params object[] args) => logger.LogError("{Time}: " + message, DateTimeOffset.UtcNow, args);
        public static void Debug(this ILogger logger, string? message, params object[] args) => logger.LogDebug("{Time}: " + message, DateTimeOffset.UtcNow, args);
    }
}
