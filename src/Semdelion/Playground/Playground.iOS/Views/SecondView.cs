using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using Semdelion.iOS.Bindings;

namespace Playground.iOS.Views
{
    public partial class SecondView : MvxViewController<SecondViewModel>
    {
        public SecondView() : base(nameof(SecondView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<SecondView, SecondViewModel>();
            set.Bind(ContentView).For(StatesTargetBinding.Key).To(vm => vm.State);
            set.Apply();
        }
    }
}

