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
    [Register ("NoDataView")]
    partial class NoDataView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView LottieView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NoDataLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LottieView != null) {
                LottieView.Dispose ();
                LottieView = null;
            }

            if (NoDataLabel != null) {
                NoDataLabel.Dispose ();
                NoDataLabel = null;
            }
        }
    }
}