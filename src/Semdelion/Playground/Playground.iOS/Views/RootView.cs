﻿using MvvmCross.Platforms.Ios.Views;

namespace Playground.iOS.Views
{
    public partial class RootView : MvxViewController<MainViewModel>
    {
        public RootView() : base("RootView", null)
        {
        }
    }
}

