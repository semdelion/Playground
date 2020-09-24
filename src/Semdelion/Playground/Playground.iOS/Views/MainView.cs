﻿using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views
{
    public partial class MainView : BaseViewController<MainViewModel>
    {
        [Outlet]
        public UIButton NextButton { get; set; }

        [Outlet]
        public UIView ContentView { get; set; }

        public MainView() : base(nameof(MainView), null)
        {
        }
        protected override void Binding()
        {
            base.Binding();
            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(NextButton).To(vm => vm.ToSecondView);
            set.Apply();
        }
    }
}

