// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Loymax.Module.ClientSettings.iOS.Custom.Cells
{
    [Register ("LogTableViewCell")]
    partial class LogTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LogLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LogLabel != null) {
                LogLabel.Dispose ();
                LogLabel = null;
            }
        }
    }
}