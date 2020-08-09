// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Semdelion.iOS.Views.States
{
    [Register ("NoInternetView")]
    partial class NoInternetView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView LottieView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NoInternetLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UpdateButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LottieView != null) {
                LottieView.Dispose ();
                LottieView = null;
            }

            if (NoInternetLabel != null) {
                NoInternetLabel.Dispose ();
                NoInternetLabel = null;
            }

            if (UpdateButton != null) {
                UpdateButton.Dispose ();
                UpdateButton = null;
            }
        }
    }
}