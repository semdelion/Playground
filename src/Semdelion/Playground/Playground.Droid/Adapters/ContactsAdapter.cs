using System;
using System.Windows.Input;
using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using FFImageLoading.Cross;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace Playground.Droid.Adapters
{
    public class ContactsAdapter : MvxRecyclerAdapter
    {
        public event EventHandler<SelectedItemEventArgs> OnItemClick;

        public ICommand CommandGetContacts { get; set; }
        public ContactsAdapter(IMvxAndroidBindingContext bindingContext) : base(bindingContext) { }

        public override int GetItemViewType(int position) => ItemTemplateSelector.GetItemViewType(GetItem(position));


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);
            View view = InflateViewForHolder(parent, viewType, itemBindingContext);

            return new SelectedItemViewHolder(view, itemBindingContext, OnClick)
            {
                Click += ItemClick,
                LongClick += ItemLongClick
            };
        }
        private void OnClick(int position, View view, object dataContext)
           => OnItemClick?.Invoke(this, new SelectedItemEventArgs(position, view, dataContext));

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var itemLogo = holder.ItemView.FindViewById<MvxCachedImageView>(Resource.Id.photo_contact_image);
            ViewCompat.SetTransitionName(itemLogo, "Transition_Name_Image" + position);

            base.OnBindViewHolder(holder, position);
            if (position >= ItemCount - 1 && CommandGetContacts.CanExecute(null))
                CommandGetContacts.Execute(null);
        }

        public class SelectedItemEventArgs : EventArgs
        {
            public SelectedItemEventArgs(int position, View view, object dataContext)
            {
                Position = position;
                View = view;
                DataContext = dataContext;
            }

            public int Position { get; }
            public View View { get; }
            public object DataContext { get; }
        }

        public class SelectedItemViewHolder : MvxRecyclerViewHolder
        {
            private readonly Action<int, View, object> _listener;

            public SelectedItemViewHolder(View itemView, IMvxAndroidBindingContext context, Action<int, View, object> listener)
                : base(itemView, context)
            {
                _listener = listener;
                ItemView.Click += ItemView_Click;
            }

            private void ItemView_Click(object sender, EventArgs e)
                => _listener(AdapterPosition, ItemView, DataContext);

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    ItemView.Click -= ItemView_Click;

                base.Dispose(disposing);
            }
        }
    }
}