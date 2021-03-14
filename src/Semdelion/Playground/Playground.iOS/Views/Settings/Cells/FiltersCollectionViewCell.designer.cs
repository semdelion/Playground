// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Playground.iOS.Views.Settings.Cells
{
    [Register ("FiltersCollectionViewCell")]
    partial class FiltersCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel FilterLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FilterLabel != null) {
                FilterLabel.Dispose ();
                FilterLabel = null;
            }
        }
    }
}