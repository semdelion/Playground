using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Pager;
using UIKit;

namespace Playground.iOS.Views.Pager
{
    [MvxPagePresentation(WrapInNavigationController = false)]
    public class Page3View : MvxViewController<Page3ViewModel>
    {
        public Page3View()
        {
            Console.WriteLine("Constructing SecondPageView");
            View.BackgroundColor = UIColor.SystemOrangeColor;
        }
    }
}
