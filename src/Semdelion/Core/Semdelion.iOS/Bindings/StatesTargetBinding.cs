namespace Semdelion.iOS.Bindings
{
    using System;
    using CoreGraphics;
    using MvvmCross.Binding.Bindings.Target;
    using Semdelion.Core.Enums;
    using Semdelion.iOS.Custom;
    using Semdelion.iOS.Extensions;
    using Semdelion.iOS.Views.States;
    using UIKit;

    public class StatesTargetBinding : MvxConvertingTargetBinding
    {
        public const string Key = "StatesTargetBinding";
        
        public StatesTargetBinding(EmptyDataSet view) : base(view)
        {
        }

        public override Type TargetType => typeof(string);

        protected override void SetValueImpl(object target, object value)
        {
            try
            {
                if (!(target is EmptyDataSet emptyDataSet) || value == null) return;
               
                States state = (States)Enum.Parse(typeof(States), value.ToString());
                UIView stateView = new UIView()
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                foreach (UIView view in emptyDataSet.ContentView.Subviews)
                    view.RemoveFromSuperview();

                emptyDataSet.ContentView.Alpha = 1f;
                var _frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

                switch (state)
                {
                    case States.Normal:
                        emptyDataSet.ContentView.Alpha = 0f;
                        break;
                    case States.Loading:
                        stateView = new LoadingView(_frame);
                        break;
                    case States.NoData:
                        stateView = new NoDataView(_frame);
                        break;
                    case States.NoInternet:
                        stateView = new NoInternetView(_frame, emptyDataSet.RefreshCommand);
                        break;
                    case States.Error:
                        stateView = new ErrorView(_frame);
                        break;
                }
                emptyDataSet.ContentView.AddSubview(stateView);
                stateView.SetCenterContraintTo(emptyDataSet.ContentView);
                stateView.SetLeftContraintTo(emptyDataSet.ContentView, 0);
                stateView.SetRightContraintTo(emptyDataSet.ContentView, 0);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
