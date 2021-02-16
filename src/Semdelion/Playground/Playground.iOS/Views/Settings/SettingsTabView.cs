using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Binding.BindingContext;
using Playground.Core.ViewModels.Settings;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views.Settings
{
    [MvxTabPresentation(TabName = "Settings", TabIconName = "home", TabSelectedIconName = "selected", WrapInNavigationController = false)]
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
            set.Bind(TextView).To(vm => vm.LogText);
            set.Apply();
        }
    }
}

