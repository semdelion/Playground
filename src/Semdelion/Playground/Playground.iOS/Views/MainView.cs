using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using UIKit;

namespace Playground.iOS.Views
{
    public partial class MainView : MvxViewController<MainViewModel>
    {
        [Outlet]
        public UIButton NextButton { get; set; }

        [Outlet]
        public UIView ContentView { get; set; }

        public MainView() : base(nameof(MainView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind().For(v => v.Title).To(vm => vm.Title);
            set.Bind(NextButton).To(vm => vm.ToSecondView);
            //set.Bind(NextButton).For("Title").ToFlyLocalizationId("MainViewModel.Title");
            //set.Bind(ContentView).For(StatesTargetBinding.Key).To(vm => vm.State);
            set.Apply();
        }
    }
}

