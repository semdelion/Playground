namespace Semdelion.Core.Log
{
    using Semdelion.Core.Log.Repository;
    using System.Collections.Generic;

    public class LogRepository : ILogRepository
    {
        private List<LogEntry> logEntries;

        public IRepository Repository { get; }

        public LogRepository(IRepository repository)
        {
            this.logEntries = new List<LogEntry>();

            Repository = repository;
        }

        public void Add(LogEntry entry)
        {
            this.logEntries.Add(entry);
        }

        public IEnumerable<LogEntry> All()
        {
            return this.logEntries;
        }

        public void RemoveAll()
        {
            this.logEntries = new List<LogEntry>();
        }
    }
}
