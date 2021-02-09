using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.ViewModels.Settings.CellElements;
using Semdelion.Core.Enums;
using Semdelion.Core.Log;
using Semdelion.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels.Settings
{
    public class LogsViewModel : BasePageCollectionViewModel<LogCellElement>
    {
        public bool Trace { get; set; } = true;
        public bool Debug { get; set; } = true;
        public bool Info { get; set; } = false;
        public bool Warn { get; set; } = true;
        public bool Error { get; set; } = true;
        public bool Fatal { get; set; } = true;

        public string SearchLine { get; set; } = "cli";

        private List<LogCellElement> Logs;

        protected ILogReader LogReader { get; set; }

        public LogsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILogReader logReader) : base(logProvider, navigationService)
        {
            LogReader = logReader;
        }

        protected override async Task DoItemClickCommand(LogCellElement item)
        {
        }

        protected override async Task<IList<LogCellElement>> LoadOnDemandItems(CancellationToken ct = default)
        {
            if (Items == null)
                State = States.Loading;

            Logs = TypeLog(LogReader.ReadAll());

            var items = SortList(Logs);

            State = items?.Count > 0 ? States.Normal : States.NoData;

            return items;
        }

        private List<LogCellElement> SortList(IList<LogCellElement> list)
        {
            var items = new List<LogCellElement>();
            foreach (var item in list)
            {
                switch(item.LogLevel)
                {
                    case MvxLogLevel.Trace:
                        if (Trace && ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                    case MvxLogLevel.Debug:
                        if (Debug && ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                    case MvxLogLevel.Info:
                        if (Info && ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                    case MvxLogLevel.Warn:
                        if (Warn && ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                    case MvxLogLevel.Error:
                        if (Error && ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                    case MvxLogLevel.Fatal:
                        if (Fatal && ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                    default:
                        if(ContainSearchText(item.LogLine))
                            items.Add(item);
                        break;
                }
            }
            return items;
        }

        public bool ContainSearchText(string text)
        {
            if (string.IsNullOrEmpty(SearchLine))
                return true;

            if (text.Contains(SearchLine, StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }


        private List<LogCellElement> TypeLog(IEnumerable<string> filter)
        {
            var items = new List<LogCellElement>();
            foreach (var log in filter)
            {
                var item = new LogCellElement { LogLine = log };

                foreach (MvxLogLevel logLevel in Enum.GetValues(typeof(MvxLogLevel)))
                {
                    if (item.LogLine.StartsWith(nameof(logLevel)))
                    {
                        item.LogLevel = logLevel;
                        break;
                    }
                }
                items.Add(item);
            }
            return items;
        }
    }
}
