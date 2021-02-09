namespace Semdelion.Core.Log
{
    using Semdelion.Core.Log.Repository;
    using System.Collections.Generic;

    public class LogRepository : ILogRepository
    {
        private List<LogEntry> _logEntries;

        public IRepository Repository { get; }

        public LogRepository(IRepository repository)
        {
            _logEntries = new List<LogEntry>();

            Repository = repository;
        }

        public void Add(LogEntry entry)
        {
            _logEntries.Add(entry);
        }

        public IEnumerable<LogEntry> All()
        {
            return _logEntries;
        }

        public void RemoveAll()
        {
            _logEntries = new List<LogEntry>();
        }
    }
}
