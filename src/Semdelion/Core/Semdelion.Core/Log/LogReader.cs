namespace Semdelion.Core.Log
{
    using MvvmCross.Binding.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LogReader : ILogReader
    {
        private string _lastEntryId;

        public ILogRepository Repository { get; }

        public event EventHandler<string> OnReaded;

        public LogReader(ILogRepository repository)
        {
            Repository = repository;
        }

        public void Read()
        {
            var text = string.Empty;

            var entries = Repository.All();

            if (!string.IsNullOrEmpty(_lastEntryId))
            {
                var existEntry = entries.FirstOrDefault(x => x.Id == _lastEntryId);
                if (existEntry != null)
                {
                    var position = entries.GetPosition(existEntry);

                    entries = entries.Skip(position).Take(entries.Count() - position);
                }
            }

            foreach (var entry in entries)
            {
                OnReaded?.Invoke(this, entry.Text);
                _lastEntryId = entry.Id;
            }
        }

        public IEnumerable<string> ReadAll()
        {
            return Repository.All().Select(x => x.Text);
        }
    }
}
