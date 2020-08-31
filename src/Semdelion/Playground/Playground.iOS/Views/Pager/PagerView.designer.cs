// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Playground.iOS.Views.Pager
{
    [Register ("PagerView")]
    partial class PagerView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem Item1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem Item2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView PagesView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBar TabBar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Item1 != null) {
                Item1.Dispose ();
                Item1 = null;
            }

            if (Item2 != null) {
                Item2.Dispose ();
                Item2 = null;
            }

            if (PagesView != null) {
                PagesView.Dispose ();
                PagesView = null;
            }

            if (TabBar != null) {
                TabBar.Dispose ();
                TabBar = null;
            }
        }
    }
}