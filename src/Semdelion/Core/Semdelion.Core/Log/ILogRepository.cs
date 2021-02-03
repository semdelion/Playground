namespace Semdelion.Core.Log
{
    using System.Collections.Generic;

    public interface ILogRepository
    {
        void Add(LogEntry entry);

        IEnumerable<LogEntry> All();

        void RemoveAll();
    }
}
