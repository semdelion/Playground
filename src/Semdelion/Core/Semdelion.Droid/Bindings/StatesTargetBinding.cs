﻿using System;
using Android.Views;
using Android.Views.Animations;
using MvvmCross.Binding;
using MvvmCross.Binding.BindingContext;
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

        object ob = new object();

        public override Type TargetType => typeof(ViewGroup);

        public View CurrentView { get; set; }

        protected override void SetValueImpl(object target, object value)
        {
            
            try
            {

                MvxLayoutInflater.Debug = true;
                if (!(target is ViewGroup view) || value == null) 
                    return;
                var inflater = new MvxLayoutInflater(view.Context);
                States state = (States)Enum.Parse(typeof(States), value.ToString());

                switch (state)
                {
                    case States.Normal:
                        ViewStateNoraml(inflater, view);
                        break;
                    case States.Loading:
                        ViewStateLoading(inflater, view);
                        break;
                    case States.NoData:
                        ViewStateNoData(inflater, view);
                        break;
                    case States.NoInternet:
                        ViewStateNoInternet(inflater, view);
                        break;
                    case States.Error:
                        ViewStateError(inflater, view);
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }

        protected virtual void ViewAppearing(ViewGroup viewGroup, View view, View newView)
        {
            if (viewGroup.Visibility == ViewStates.Gone)
                viewGroup.Visibility = ViewStates.Visible;

            if (view != null && view.Id == newView.Id)
                return;

            if (view != null)
            {
                view.StartAnimation(AnimationUtils.LoadAnimation(viewGroup.Context, Resource.Animation.abc_fade_out));
                view.Animation.AnimationEnd += (o, s) => viewGroup.RemoveView(view);
            }

            CurrentView = newView;
            CurrentView.StartAnimation(AnimationUtils.LoadAnimation(viewGroup.Context, Resource.Animation.abc_fade_in));
            viewGroup.AddView(CurrentView);
        }

        protected virtual void ViewDisappearing(ViewGroup viewGroup)
        {
            viewGroup.StartAnimation(AnimationUtils.LoadAnimation(viewGroup.Context, Resource.Animation.abc_fade_out));
            viewGroup.Animation.AnimationEnd += (o, s) =>
            {
                if (CurrentView != null)
                {
                    viewGroup.RemoveView(CurrentView);
                    CurrentView.Dispose();
                    CurrentView = null;
                }
                viewGroup.Visibility = ViewStates.Gone;
            };
           
        }

        protected virtual void ViewStateNoraml(MvxLayoutInflater inflater, ViewGroup view)
        {
            ViewDisappearing(view);
        }

        protected virtual void ViewStateLoading(MvxLayoutInflater inflater, ViewGroup view)
        {
            ViewAppearing(view, CurrentView, inflater.Inflate(Resource.Layout.state_loading, view, false));
        }

        protected virtual void ViewStateNoData(MvxLayoutInflater inflater, ViewGroup view)
        {
            ViewAppearing(view, CurrentView, inflater.Inflate(Resource.Layout.state_no_date, view, false));
        }

        protected virtual void ViewStateNoInternet(MvxLayoutInflater inflater, ViewGroup view)
        {
            ViewAppearing(view, CurrentView, inflater.Inflate(Resource.Layout.state_no_internet, view, false));
        }

        protected virtual void ViewStateError(MvxLayoutInflater inflater, ViewGroup view)
        {
            ViewAppearing(view, CurrentView, inflater.Inflate(Resource.Layout.state_error, view, false));
        }
    }
}