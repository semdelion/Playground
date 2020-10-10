namespace Semdelion.iOS.Views.Base
{
    using Foundation;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Platforms.Ios.Views;
    using Semdelion.Core.ViewModels.Interfaces;
    using Semdelion.iOS.Views.Interfaces;

    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>, IBindingTitleViewController
        where TViewModel : class, IBaseViewModel
    {
        public virtual bool NavigationBarHidden => false;
        public bool BindingTitle { get; set; } = true;

        protected BaseViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        protected BaseViewController()
        {
        }

        public sealed override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ConfigureViews();
            Binding();
        }

        protected virtual void Binding()
        {
            if (BindingTitle)
                this.CreateBinding(this).For(v => v.Title).To<TViewModel>(vm => vm.Title).Apply();
        }

        protected virtual void ConfigureViews()
        {
        }
    }
}
