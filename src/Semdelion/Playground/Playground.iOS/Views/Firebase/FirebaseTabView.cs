﻿using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Firebase;
using Semdelion.iOS.Views.Base;

namespace Playground.iOS.Views.Firebase
{
    [MvxTabPresentation(TabName = "Firebase", TabIconName = "ic_notification", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class FirebaseTabView : BaseViewController<FirebaseViewModel>
    {
		public FirebaseTabView() : base(nameof(FirebaseTabView), null)
		{
		}

        protected override void ConfigureViews()
        {
            base.ConfigureViews();
            tokenTextView.Text = Semdelion.Core.User.Settings.TokenFcm;
        }
        
    }
}