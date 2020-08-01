using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Target;
using MvvmCross.Platforms.Android.Binding.Views;
using Semdelion.Core.Enums;

namespace Semdelion.Droid.Bindings
{
    public class StatesTargetBinding : MvxAndroidTargetBinding
    {
        public const string Key = "StatesTargetBinding";

        public StatesTargetBinding(View view) : base(view)
        {
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        public override Type TargetType => typeof(ViewGroup);

        public View PrevView = null;

        protected override void SetValueImpl(object target, object value)
        {
            try
            {
                if (!(target is View view) || value == null) 
                    return;
                var inflate = new MvxLayoutInflater(view.Context);
                if (PrevView != null)
                {
                    ((ViewGroup)view).RemoveView(PrevView);
                    PrevView = null;
                }
                States state = (States)Enum.Parse(typeof(States), value.ToString());
                switch (state)
                {
                    case States.Normal:
                        PrevView = inflate.Inflate(Resource.Layout.Empty, null, false);
                        ((ViewGroup)view).AddView(PrevView);
                        break;
                    case States.Loading:
                        break;
                    case States.NoData:
                        PrevView = inflate.Inflate(Resource.Layout.Update, null, false);
                        ((ViewGroup)view).AddView(PrevView);
                        break;
                    case States.NoInternet:
                        break;
                    case States.Error:
                        break;
                }
              
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}