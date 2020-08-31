using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Pager;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace Playground.iOS.Views.Pager
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public class PagesRootView : MvxPageViewController<PagesRootViewModel>, IMvxIosView
    { 

         private bool _isPresentedFirstTime = true;

        public PagesRootView() : base(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CreateUI();
          //  var set = this.CreateBindingSet<PagesRootView, PagesRootViewModel>();
          //  set.Apply();
        }

        private void CreateUI()
        {
            EdgesForExtendedLayout = UIRectEdge.None;
            
            View.BackgroundColor = UIColor.White;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (ViewModel != null && _isPresentedFirstTime)
            {
                _isPresentedFirstTime = false;
                ViewModel.ShowInitialViewModelsCommand.ExecuteAsync(null);
            }
        }

        protected override UIViewController GetNextViewControllerPage(UIViewController rc)
        {
            return base.GetNextViewControllerPage(rc);
        }

        protected override UIViewController GetPreviousViewControllerPage(UIViewController rc)
        {
            return base.GetPreviousViewControllerPage(rc);
        }
    }
}
