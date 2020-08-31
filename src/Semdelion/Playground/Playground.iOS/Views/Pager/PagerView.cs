using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels.Pager;
using Playground.iOS.Custom.Pages;
using Playground.iOS.Custom.ScrollablePage;
using UIKit;

namespace Playground.iOS.Views.Pager
{
   /* public partial class PagerView : MvxViewController<PagerViewModel>
    {
        public PagerView() : base(nameof(PagerView), null)
        {
        }

        public sealed override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var _pages = new List<IPageViewControllerIndex>
            {
                new Page1View() { },
                new Page2View() { }
            };

            var Pager = new PageViewController(new ScrollablePageDataSource(_pages, TabBar));

            Pager.View.Frame = PagesView.Bounds;
            PagesView.AddSubview(Pager.View);

            TabBar.SelectedItem = TabBar.Items![0];
            TabBar.ItemSelected += (object sender, UITabBarItemEventArgs e) =>
            {
                if ((Pager.ViewControllers[0] is IPageViewControllerIndex step))
                {
                    if (e.Item.Tag == 1 && step.Index == 0)
                    {
                        Pager?.SetNextItem();
                    }
                    if (e.Item.Tag == 0 && step.Index == 1)
                    {
                        Pager?.SetPreviousItem();
                    }
                }
            };
        }
    }*/
}

