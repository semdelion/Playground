using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
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
        private bool _trace = true;
        private bool _debug = true;
        private bool _info = true;
        private bool _warn = true;
        private bool _error = true;
        private bool _fatal = true;

        public bool Trace 
        {
            get => _trace;
            set
            {
                SetProperty(ref _trace, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }

        public bool Debug
        {
            get => _debug;
            set
            {
                SetProperty(ref _debug, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }

        public bool Info
        {
            get => _info;
            set
            {
                SetProperty(ref _info, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }

        public bool Warn
        {
            get => _warn;
            set
            {
                SetProperty(ref _warn, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }
       
        public bool Error
        {
            get => _error;
            set
            {
                SetProperty(ref _error, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }

        public bool Fatal
        {
            get => _fatal;

            set
            {
                SetProperty(ref _fatal, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }

        private string _searchLine = string.Empty;
        public string SearchLine 
        {
            get => _searchLine;

            set
            {
                SetProperty(ref _searchLine, value);
                Items = new MvxObservableCollection<LogCellElement>(SortList(Logs));
            }
        }

        private IList<LogCellElement> Logs;

        protected ILogReader LogReader { get; set; }

        public LogsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILogReader logReader) : base(logProvider, navigationService)
        {
            LogReader = logReader;
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
