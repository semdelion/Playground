using Foundation;
using Loymax.Module.ClientSettings.iOS.Custom.Cells;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace Playground.iOS.Views.Settings.Sources
{
    public class LogsTableViewSource : MvxTableViewSource
    {
        public LogsTableViewSource(UITableView tableView) : base(tableView)
        {
            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            tableView.RegisterNibForCellReuse(UINib.FromName(nameof(LogTableViewCell), NSBundle.MainBundle), LogTableViewCell.Key);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
            => tableView.DequeueReusableCell(LogTableViewCell.Key, indexPath);
    }
}
