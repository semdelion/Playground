// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Playground.iOS.Views.Firebase
{
	[Register ("FirebaseTabView")]
	partial class FirebaseTabView
	{
		[Outlet]
		UIKit.UITextView tokenTextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tokenTextView != null) {
				tokenTextView.Dispose ();
				tokenTextView = null;
			}
		}
	}
}
