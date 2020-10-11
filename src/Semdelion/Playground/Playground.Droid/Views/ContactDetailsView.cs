using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Transitions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Phonebook;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainFragmentHostViewModel), Resource.Id.main_layoutContent, true)]
    [Register(nameof(ContactDetailsView))]
    public class ContactDetailsView : BaseFragment<ContactDetailsViewModel>
    {
        protected override int FragmentId => Resource.Layout.contact_details_view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                SharedElementEnterTransition = TransitionInflater.From(Activity).InflateTransition(Android.Resource.Transition.Move);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);
            Arguments.SetSharedElementsByTag(view);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            (Activity as AppCompatActivity)?.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Activity.OnBackPressed();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}