﻿using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Firebase;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views.Firebase
{
    [MvxTabPresentation(TabName = "Firebase", TabIconName = "home", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class FirebaseTabView : BaseViewController<FirebaseViewModel>
    {
        protected override void ConfigureViews()
        {
            base.ConfigureViews();
        }
    }
}

