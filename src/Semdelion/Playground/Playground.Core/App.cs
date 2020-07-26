﻿using MvvmCross.ViewModels;
using Playground.Core.ViewModels;

namespace Playground.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<MainViewModel>();
        }
    }
}
