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

namespace Playground.iOS.Views
{
    [Register ("SecondView")]
    partial class SecondView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ContentView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ContentView != null) {
                ContentView.Dispose ();
                ContentView = null;
            }
        }
    }
}