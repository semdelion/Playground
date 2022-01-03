
using Microsoft.Extensions.Logging;

namespace Playground.Core.ViewModels.Settings.CellElements
{
    public class LogCellElement
    {
        public string LogLine { get; set; }

        public LogLevel? LogLevel { get; set; } = null;
    }
}
