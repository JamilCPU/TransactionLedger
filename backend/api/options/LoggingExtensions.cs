namespace Backend.api.options
{
    public static class LoggingExtensions
    {
        // Conditionally log based on flag - Information level
        public static void LogInformationIfEnabled(this ILogger logger, LoggingOptions options, string message, params object[] args)
        {
            if (options.EnableDetailedLogging)
            {
                logger.LogInformation(message, args);
            }
        }

        // Conditionally log based on flag - Warning level
        public static void LogWarningIfEnabled(this ILogger logger, LoggingOptions options, string message, params object[] args)
        {
            if (options.EnableDetailedLogging)
            {
                logger.LogWarning(message, args);
            }
        }

        // Conditionally log based on flag - Debug level
        public static void LogDebugIfEnabled(this ILogger logger, LoggingOptions options, string message, params object[] args)
        {
            if (options.EnableDetailedLogging)
            {
                logger.LogDebug(message, args);
            }
        }

        // Conditionally log based on flag - Trace level
        public static void LogTraceIfEnabled(this ILogger logger, LoggingOptions options, string message, params object[] args)
        {
            if (options.EnableDetailedLogging)
            {
                logger.LogTrace(message, args);
            }
        }

        // Note: LogError should always be called directly - errors are always logged regardless of flag
    }
}

