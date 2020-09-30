using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Droid.Adapters;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainFragmentHostViewModel), Resource.Id.main_layoutContent, true)]
    [Register(nameof(SecondView))]
    public class SecondView : BaseFragment<SecondViewModel>
    {
        protected override int FragmentId => Resource.Layout.second_view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var recyclerAdapter = new ContactsAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerAdapter.CommandGetContacts = ViewModel.LoadNextPage;
            //recyclerAdapter.OnItemClick += Adapter_OnItemClick;
            view.FindViewById<MvxRecyclerView>(Resource.Id.recyclerView).Adapter = recyclerAdapter;
            return view;
        }
        //private void Adapter_OnItemClick(object sender, SelectedItemRecyclerAdapter.SelectedItemEventArgs e)
        //{
        //    Toast.MakeText(Activity, $"Selected item {e.Position + 1}", ToastLength.Short)
        //        .Show();

        //    ImageView itemLogo = e.View.FindViewById<ImageView>(Resource.Id.img_logo);
        //    itemLogo.Tag = Activity.Resources.GetString(Resource.String.transition_list_item_icon);

        //    ViewModel.SelectItemExecution(e.DataContext as ListItemViewModel);
        //}
    }
}