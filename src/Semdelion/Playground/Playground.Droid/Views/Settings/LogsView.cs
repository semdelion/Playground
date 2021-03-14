using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Settings;
using Playground.Droid.Adapters;
using Semdelion.Core.Helpers;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views.Settings
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, true)]
    [Register(nameof(LogsView))]
    public class LogsView : BaseFragment<LogsViewModel> 
    {
        protected override int FragmentId => Resource.Layout.logs_view;

        protected override void SetView(View view)
        {
            var toolBarLayout = view.FindViewById<Toolbar>(Resource.Id.log_toolbar);
            ((AppCompatActivity)Activity).SetSupportActionBar(toolBarLayout);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            toolBarLayout.NavigationClick += (o, s) => ViewModel.CancelCommand.Execute();
            CreateSearchView(view);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.logs_recycler_view);
            var recyclerAdapter = new LogsAdapter((IMvxAndroidBindingContext)BindingContext, Context);
            recyclerView.Adapter = recyclerAdapter;
        }

        private void CreateSearchView(View view)
        {
            var searchViewLayout = this.BindingInflate(Resource.Layout._template_search_view, null, false);
            var searchTextView = searchViewLayout.FindViewById<Android.Widget.EditText>(Resource.Id.search_text);
            var searchCloseButton = searchViewLayout.FindViewById<Android.Widget.ImageButton>(Resource.Id.search_close_imageButton);
            var searchStartButton = searchViewLayout.FindViewById<Android.Widget.ImageButton>(Resource.Id.search_start_imageButton);

            searchTextView.Hint = Localize.GetText("LogsViewModel.Placeholders.Search");

            searchTextView.TextChanged += (sender, args) =>
            {
                ViewModel.SearchLine = searchTextView.Text;
            };

            searchCloseButton.Click += (sender, args) =>
            {
                ViewModel.SearchLine = searchTextView.Text = "";
                ViewModel.SearchHide = !ViewModel.SearchHide;

                if (searchViewLayout.LayoutParameters is Toolbar.LayoutParams parameters)
                {
                    parameters.Width = ViewGroup.LayoutParams.WrapContent;
                    parameters.Height = ViewGroup.LayoutParams.MatchParent;
                    parameters.Gravity = (int)GravityFlags.End;
                    searchViewLayout.LayoutParameters = parameters;
                }
            };

            searchStartButton.Click += (sender, args) =>
            {
                ViewModel.SearchHide = !ViewModel.SearchHide;

                var imm = Context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;

                searchTextView.FocusChange += (sender, args) =>
                {
                    if (args.HasFocus) return;
                    imm?.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
                };

                searchTextView.RequestFocus();
                imm?.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);

                if (searchViewLayout.LayoutParameters is Toolbar.LayoutParams parameters)
                {
                    parameters.Width = ViewGroup.LayoutParams.MatchParent;
                    parameters.Height = ViewGroup.LayoutParams.MatchParent;
                    searchViewLayout.LayoutParameters = parameters;
                }
            };

            view.FindViewById<Toolbar>(Resource.Id.log_toolbar).AddView(searchViewLayout);

            if (searchViewLayout.LayoutParameters is Toolbar.LayoutParams parameters)
            {
                parameters.Width = ViewGroup.LayoutParams.WrapContent;
                parameters.Height = ViewGroup.LayoutParams.MatchParent;
                parameters.Gravity = (int)GravityFlags.End;
                searchViewLayout.LayoutParameters = parameters;
            }
        }
    }
}