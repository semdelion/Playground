namespace Semdelion.iOS.Bindings
{
    using System;
    using MvvmCross.Binding.Bindings.Target;
    using Semdelion.Core.Enums;
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
                if (!(target is UIView view) || value == null) return;

                States state = (States)Enum.Parse(typeof(States), value.ToString());
                UIView stateView = new UIView();
                switch (state)
                {
                    case States.Clean:
                        stateView = new NoDataView();
                        break;
                    case States.Normal:
                        stateView = new NoDataView();
                        break;
                    case States.Loading:
                        stateView = new LoadingView();
                        break;
                    case States.NoData:
                        stateView = new NoDataView();
                        break;
                    case States.NoInternet:
                        stateView = new NoDataView();
                        break;
                    case States.Error:
                        stateView = new NoDataView();
                        break;
                }
                view.AddSubview(stateView);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
