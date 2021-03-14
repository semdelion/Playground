using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Settings;
using Semdelion.iOS.Views.Base;

namespace Playground.iOS.Views.Settings
{
    [MvxTabPresentation(TabName = "Settings", TabIconName = "ic_settings", TabSelectedIconName = "selected", WrapInNavigationController = true)]
    public partial class SettingsTabView : BaseViewController<SettingsViewModel>
    {
        public SettingsTabView() : base(nameof(SettingsTabView), null)
        {
        }

        protected override void ConfigureViews()
        {
            base.ConfigureViews();
        }

        protected override void Binding()
        {
            base.Binding();

            var set = this.CreateBindingSet<SettingsTabView, SettingsViewModel>();
            set.Bind(LogsButton).To(vm => vm.OpenLogsCommand);
            set.Apply();
        }
    }
}

