using MvvmCross.Logging;

namespace Playground.Core.ViewModels.Settings.CellElements
{
    public class LogCellElement
    {
        public string LogLine { get; set; }

        public MvxLogLevel? LogLevel { get; set; } = null;
    }
}
