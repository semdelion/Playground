namespace Semdelion.iOS.Log
{
    using System;
    using System.Diagnostics;
    using MvvmCross;
    using Semdelion.Core.Log;
    using Semdelion.Core.Extensions;
    using Microsoft.Extensions.Logging;

    public class DebugTrace : ILogger
    {
        private ILogWriter logWriter;
        protected ILogWriter LogWriter => Mvx.IoCProvider.CanResolve<ILogWriter>() ? (logWriter ??= Mvx.IoCProvider.Resolve<ILogWriter>()) : null;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                var logEntry = string.Empty;

                if (formatter == null)
                {
                    logEntry = $"{logLevel}:" + (exception?.BuildAllMessagesAndStackTrace() ?? string.Empty);
                }
                else
                {
                    logEntry = logLevel + ":" + formatter.Invoke(state, exception);
                }

                Debug.WriteLine(logEntry);

                LogWriter?.Write(logEntry);
            }
            catch (Exception)
            {
                this.Log(LogLevel.Trace, $"Exception during trace of {logLevel} {exception.Message}");
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new Disposable();
        }

        internal class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}