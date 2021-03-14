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

namespace Playground.iOS.Views.Settings
{
    [Register ("SettingsTabView")]
    partial class SettingsTabView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LogsButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LogsButton != null) {
                LogsButton.Dispose ();
                LogsButton = null;
            }
        }
    }
}