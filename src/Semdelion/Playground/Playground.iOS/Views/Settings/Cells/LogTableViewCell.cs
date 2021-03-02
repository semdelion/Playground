namespace Loymax.Module.ClientSettings.iOS.Custom.Cells
{
    using System;
    using Foundation;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Logging;
    using MvvmCross.Platforms.Ios.Binding.Views;
    using Playground.Core.ViewModels.Settings.CellElements;
    using UIKit;

    public partial class LogTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(LogTableViewCell));
        public static readonly UINib Nib;

        static LogTableViewCell()
        {
            Nib = UINib.FromName(nameof(LogTableViewCell), NSBundle.MainBundle);
        }

        public MvxLogLevel ColorText
        {
            set
            {
                if (value == MvxLogLevel.Warn)
                    LogLabel.TextColor = UIColor.Orange;
                else if (value == MvxLogLevel.Error)
                    LogLabel.TextColor = UIColor.Red;
                else if (value == MvxLogLevel.Fatal)
                    LogLabel.TextColor = UIColor.Red;
                else
                    LogLabel.TextColor = UIColor.White;
            }
        }

        protected LogTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<LogTableViewCell, LogCellElement>();
                set.Bind(LogLabel).To(vm => vm.LogLine);
                set.Bind(this).For("ColorText").To(vm => vm.LogLevel);
                set.Apply();
            });
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            BackgroundColor = UIColor.Clear;
        }
    }
}
