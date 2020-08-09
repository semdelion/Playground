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
    [Register ("LoadingView")]
    partial class LoadingView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView Loader { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Loader != null) {
                Loader.Dispose ();
                Loader = null;
            }
        }
    }
}