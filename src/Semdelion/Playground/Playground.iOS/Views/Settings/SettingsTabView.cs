using System.Threading.Tasks;
using Airbnb.Lottie;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Settings;
using Semdelion.iOS.Extensions;
using Semdelion.iOS.Views.Base;
using UIKit;
using Semdelion.Core.Extensions;

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
            set.Bind(ThemeButton).For("Title").ToFlyLocalizationId("SettingsViewModel.NightMode");
            set.Bind(LogsButton).To(vm => vm.OpenLogsCommand);
            set.Bind(LogsButton).For("Title").ToFlyLocalizationId("SettingsViewModel.Logs");
            set.Bind(LanguageButton).To(vm => vm.ChangeLocale);
            set.Bind(LanguageButton).For("Title").To(vm => vm.CurrentCulture);
            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            LOTAnimatedSwitch lottie = LOTAnimatedSwitch.SwitchNamed("nightmode");
            float state1 = Semdelion.Core.User.Settings.ModeNight ? 0.9f : 0.5f;
            float state2 = Semdelion.Core.User.Settings.ModeNight ? 0.5f : 0.1f;
            lottie.SetProgressRangeForOnState(state2, state1);
            lottie.SetProgressRangeForOffState(state1, state2);
            lottie.ContentMode = UIViewContentMode.ScaleToFill;
            lottie.Frame = new CoreGraphics.CGRect(0, 0, ThemeView.Frame.Width, ThemeView.Frame.Height);
            
            ThemeView.AddSubview(lottie);
            lottie.ValueChanged += (sender, e) =>
            {
                var window = UIApplication.SharedApplication.Windows[0];
                window.OverrideUserInterfaceStyle = Semdelion.Core.User.Settings.ModeNight ? UIUserInterfaceStyle.Light : UIUserInterfaceStyle.Dark;
                Semdelion.Core.User.Settings.ModeNight = !Semdelion.Core.User.Settings.ModeNight;
            };
        }
    }
}