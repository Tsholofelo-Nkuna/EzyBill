using System.Reflection;

namespace EzyBill.API
{
    public class FileLoggingProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName);
        }

        public void Dispose()
        {
           
        }
    }

    public class FileLogger : ILogger
    {
        private readonly Object _lock = new object();
        private readonly string _categoryName;
        public FileLogger(string categoryName) { 
            this._categoryName = categoryName;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
           return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
          return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{DateTime.Now.ToString("yyMMdd")}.log");
                File.AppendAllLines(filePath, [$"At {DateTime.Now} "+_categoryName +" said:",exception?.Message ?? string.Empty, formatter(state, exception)]);
            }
        }
    }
}
