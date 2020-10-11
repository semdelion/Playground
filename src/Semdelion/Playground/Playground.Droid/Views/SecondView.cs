using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Cross;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Phonebook;
using Playground.Droid.Adapters;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainFragmentHostViewModel), Resource.Id.main_layoutContent, true)]
    [Register(nameof(SecondView))]
    public class SecondView : BaseFragment<ContactsViewModel>
    {
        protected override int FragmentId => Resource.Layout.second_view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var recyclerAdapter = new ContactsAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerAdapter.OnItemClick += Adapter_OnItemClick;
            recyclerAdapter.CommandGetContacts = ViewModel.LoadNextPage;
            view.FindViewById<MvxRecyclerView>(Resource.Id.recyclerView).Adapter = recyclerAdapter;
            return view;
        }

        private void Adapter_OnItemClick(object sender, ContactsAdapter.SelectedItemEventArgs e)
        {
            var itemLogo = e.View.FindViewById<MvxCachedImageView>(Resource.Id.contact_photo_image_view);
            itemLogo.Tag = Activity.Resources.GetString(Resource.String.transition_contact_photo);

            var itemName = e.View.FindViewById<TextView>(Resource.Id.contact_full_name);
            itemName.Tag = Activity.Resources.GetString(Resource.String.transition_contact_full_name);

            var itemPhone = e.View.FindViewById<TextView>(Resource.Id.contact_phone_number);
            itemPhone.Tag = Activity.Resources.GetString(Resource.String.transition_contact_phone);
        }
    }
}