namespace Semdelion.Core.Log
{
    using System;

    public class LogWriter : ILogWriter
    {
        public ILogRepository Repository { get; }

        public event EventHandler OnWritten;

        public LogWriter(ILogRepository repository)
        {
            Repository = repository;
        }

        public void Write(string entry)
        {
            Repository.Add(new LogEntry
            {
                Id = Guid.NewGuid().ToString(),
                Text = entry
            });

            OnWritten?.Invoke(this, EventArgs.Empty);
        }
    }
}
