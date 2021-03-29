// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Playground.iOS.Views.Settings
{
	[Register ("SettingsTabView")]
	partial class SettingsTabView
	{
		[Outlet]
		UIKit.UIButton LanguageButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton LogsButton { get; set; }

		[Outlet]
		UIKit.UIButton ThemeButton { get; set; }

		[Outlet]
		UIKit.UIView ThemeView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LogsButton != null) {
				LogsButton.Dispose ();
				LogsButton = null;
			}

			if (ThemeButton != null) {
				ThemeButton.Dispose ();
				ThemeButton = null;
			}

			if (LanguageButton != null) {
				LanguageButton.Dispose ();
				LanguageButton = null;
			}

			if (ThemeView != null) {
				ThemeView.Dispose ();
				ThemeView = null;
			}
		}
	}
}
