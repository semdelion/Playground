﻿using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using FFImageLoading.Cross;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Droid.Adapters;
using Semdelion.API.Models;
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
            recyclerAdapter.OnItemClick += Adapter_OnItemClick;
            recyclerAdapter.CommandGetContacts = ViewModel.LoadNextPage;
            view.FindViewById<MvxRecyclerView>(Resource.Id.recyclerView).Adapter = recyclerAdapter;
            return view;
        }

        private void Adapter_OnItemClick(object sender, ContactsAdapter.SelectedItemEventArgs e)
        {
            MvxCachedImageView itemLogo = e.View.FindViewById<MvxCachedImageView>(Resource.Id.photo_contact_image);
            itemLogo.Tag = Activity.Resources.GetString(Resource.String.transition_list_item_icon);

            ViewModel.ItemClickCommand.Execute(e.DataContext as Contact);
        }
    }
}