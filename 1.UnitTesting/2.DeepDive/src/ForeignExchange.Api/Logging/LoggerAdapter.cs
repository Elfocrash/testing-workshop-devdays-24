namespace ForeignExchange.Api.Logging;

public interface ILoggerAdapter<T>
{
    void LogInformation(string messageTemplate, params object?[] args);
}

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string messageTemplate, params object?[] args)
    {
        _logger.LogInformation(messageTemplate, args);
    }
}
