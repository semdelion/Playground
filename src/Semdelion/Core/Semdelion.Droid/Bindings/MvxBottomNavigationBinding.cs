using System;
using Google.Android.Material.BottomNavigation;
using MvvmCross.Binding;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Binding.Target;

namespace Semdelion.Droid.Bindings
{
    public class MvxBottomNavigationBinding : MvxAndroidTargetBinding
    {
        public const string Key = "BottomNavigationSelectedBindingKey";

        readonly BottomNavigationView _bottomNav;
        IMvxCommand _command;

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public override Type TargetType => typeof(MvxCommand);

        public MvxBottomNavigationBinding(BottomNavigationView bottomNav) : base(bottomNav)
        {
            _bottomNav = bottomNav;
            _bottomNav.ItemSelected += OnNavigationItemSelected;
            //_bottomNav.NavigationItemSelected += OnNavigationItemSelected;
        }

        public override void SetValue(object value)
        {
            _command = (IMvxCommand)value;
        }

        protected override void SetValueImpl(object target, object value)
        {

        }

        void OnNavigationItemSelected(object sender, BottomNavigationView.ItemSelectedEventArgs e)
        {

            if (_command != null)
                _command.Execute(e.Item.TitleCondensedFormatted.ToString());
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
                _bottomNav.ItemSelected -= OnNavigationItemSelected;

            base.Dispose(isDisposing);
        }
    }
}