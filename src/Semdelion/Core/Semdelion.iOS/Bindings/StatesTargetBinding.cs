namespace Semdelion.iOS.Bindings
{
    using System;
    using CoreGraphics;
    using MvvmCross.Binding.Bindings.Target;
    using Semdelion.Core.Enums;
    using Semdelion.iOS.Extensions;
    using Semdelion.iOS.Views.States;
    using UIKit;

    public class StatesTargetBinding : MvxConvertingTargetBinding
    {
        public const string Key = "StatesTargetBinding";
        
        public StatesTargetBinding(UIView view) : base(view)
        {
        }

        public override Type TargetType => typeof(string);

        protected override void SetValueImpl(object target, object value)
        {
            try
            {
                if (!(target is UIView contentView) || value == null) return;
               
                States state = (States)Enum.Parse(typeof(States), value.ToString());
                UIView stateView = new UIView()
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                foreach (UIView view in contentView.Subviews)
                    view.RemoveFromSuperview();

                contentView.Alpha = 1f;
                var _frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

                switch (state)
                {
                    case States.Normal:
                        contentView.Alpha = 0f;
                        break;
                    case States.Loading:
                        stateView = new LoadingView(_frame);
                        break;
                    case States.NoData:
                        stateView = new NoDataView(_frame);
                        break;
                    case States.NoInternet:
                        stateView = new NoInternetView(_frame);
                        break;
                    case States.Error:
                        stateView = new ErrorView(_frame);
                        break;
                }
                contentView.AddSubview(stateView);
                stateView.SetCenterContraintTo(contentView);// SetFillYContraintTo(contentView, 16);
                stateView.SetLeftContraintTo(contentView, 0);
                stateView.SetRightContraintTo(contentView, 0);
                contentView.SetNeedsUpdateConstraints();
                contentView.LayoutIfNeeded();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
