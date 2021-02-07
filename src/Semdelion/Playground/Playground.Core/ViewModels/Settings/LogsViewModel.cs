using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.ViewModels.Settings.CellElements;
using Semdelion.Core.Enums;
using Semdelion.Core.Extensions;
using Semdelion.Core.Log;
using Semdelion.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels.Settings
{
    public class LogsViewModel : BasePageCollectionViewModel<LogCellElement>
    {
       
        protected ILogReader LogReader { get; set; }

        public LogsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILogReader logReader, ILogRepository logRepository) : base(logProvider, navigationService)
        {

            LogReader = logReader;
        }

        protected override async Task DoItemClickCommand(LogCellElement item)
        {
            //throw new NotImplementedException();
        }

        protected override async Task<IList<LogCellElement>> LoadOnDemandItems(CancellationToken ct = default)
        {
            if (Items == null)
                State = States.Loading;

            var items = new List<LogCellElement>();

            var logs = LogReader.ReadAll();
            foreach (var log in logs)
                items.Add(new LogCellElement() { LogLine = log });
          
            State = items?.Count > 0 ? States.Normal : States.NoData;

            return items;
        }
    }
}
