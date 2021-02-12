using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Settings;
using Semdelion.Core.Helpers;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views.Settings
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, true)]
    [Register(nameof(LogsView))]
    public class LogsView : BaseFragment<LogsViewModel> 
    {
        protected override int FragmentId => Resource.Layout.logs_view;

        private View SearchView { get; set; }
        private View SearchIcon { get; set; }

        private Android.Widget.EditText TextView { get; set; }

        private Toolbar ToolBarLayout { get; set; }

        protected override void SetView(View view)
        {
            ToolBarLayout = view.FindViewById<Toolbar>(Resource.Id.log_toolbar);
            ((AppCompatActivity)Activity).SetSupportActionBar(ToolBarLayout);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ToolBarLayout.NavigationClick += (o, s) => ViewModel.CancelCommand.Execute();
            CreateSearchView(view);
        }

        public override void OnResume()
        {
            base.OnResume();
            AddSearchIconView();
        }

        public override void OnPause()
        {
            base.OnPause();
            RemoveFromParent(SearchIcon);
            RemoveFromParent(SearchView);
        }

        private void AddSearchView()
        {
            RemoveFromParent(SearchIcon);
            ToolBarLayout.AddView(SearchView, 0);

            if (SearchView.LayoutParameters is Toolbar.LayoutParams parameters)
            {
                parameters.Width = ViewGroup.LayoutParams.MatchParent;
                parameters.Height = ViewGroup.LayoutParams.MatchParent;

                SearchView.LayoutParameters = parameters;
            }

            TextView.Text = ViewModel.SearchLine;
            TextView.SetSelection(ViewModel.SearchLine.Length);
        }

        private void AddSearchIconView()
        {
            RemoveFromParent(SearchView);
            ToolBarLayout.AddView(SearchIcon, 0);

            if (SearchIcon.LayoutParameters is Toolbar.LayoutParams parameters)
            {
                parameters.Width = ViewGroup.LayoutParams.WrapContent;
                parameters.Height = ViewGroup.LayoutParams.MatchParent;
                parameters.Gravity = (int)GravityFlags.End;

                SearchIcon.LayoutParameters = parameters;
            }
        }

        private void CreateSearchView(View view)
        {
            SearchView = LayoutInflater.Inflate(Droid.Resource.Layout._template_search_view, null);
            TextView = SearchView.FindViewById<Android.Widget.EditText>(Droid.Resource.Id.search_text);
            var imageButton = SearchView.FindViewById<Android.Widget.ImageButton>(Droid.Resource.Id.imageButton);
            TextView.Hint = Localize.GetText("LogsViewModel.Placeholders.Search");

            TextView.TextChanged += (sender, args) =>
            {
                ViewModel.SearchLine = TextView.Text;
            };

            imageButton.Click += (sender, args) =>
            {
                if (TextView.Visibility == ViewStates.Gone && string.IsNullOrEmpty(TextView.Text))
                    TextView.Visibility = ViewStates.Visible;
                if (string.IsNullOrEmpty(TextView.Text))
                    AddSearchIconView();

                ViewModel.SearchLine = TextView.Text = "";
            };

            SearchIcon = LayoutInflater.Inflate(Droid.Resource.Layout._template_search_icon_view, null);
            var imageButtonSearch = SearchIcon.FindViewById<Android.Widget.ImageButton>(Droid.Resource.Id.imageButton);

            imageButtonSearch.Click += (sender, args) =>
            {
                AddSearchView();
                var imm = Context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;

                TextView.FocusChange += (sender, args) =>
                {
                    if (args.HasFocus) return;
                    imm?.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
                };

                TextView.RequestFocus();
                imm?.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            };
        }

        public void RemoveFromParent(View view)
        {
            (view.Parent as ViewGroup)?.RemoveView(view);
        }
    }
}