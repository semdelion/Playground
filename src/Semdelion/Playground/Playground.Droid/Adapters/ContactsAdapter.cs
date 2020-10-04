using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using FFImageLoading.Cross;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System;
using System.Windows.Input;

namespace Playground.Droid.Adapters
{
    public class ContactsAdapter : MvxRecyclerAdapter
    {
        private const string _photoTransitionName = "_photoTransitionName";
        private const string _fullNameTransitionName = "_fullNameTransitionName";
        private const string _phoneTransitionName = "_phoneTransitionName";

        public event EventHandler<SelectedItemEventArgs> OnItemClick;

        public ICommand CommandGetContacts { get; set; }

        public ContactsAdapter(IMvxAndroidBindingContext bindingContext) : base(bindingContext) 
        { 
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);
            View view = InflateViewForHolder(parent, viewType, itemBindingContext);

            return new SelectedItemViewHolder(view, itemBindingContext, OnClick);
        }
        private void OnClick(int position, View view, object dataContext)
           => OnItemClick?.Invoke(this, new SelectedItemEventArgs(position, view, dataContext));

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var photo = holder.ItemView.FindViewById<MvxCachedImageView>(Resource.Id.contact_photo_image_view);
            ViewCompat.SetTransitionName(photo, _photoTransitionName + position);
            var phone = holder.ItemView.FindViewById<TextView>(Resource.Id.contact_phone_number);
            ViewCompat.SetTransitionName(phone, _fullNameTransitionName + position);
            var name = holder.ItemView.FindViewById<TextView>(Resource.Id.contact_full_name);
            ViewCompat.SetTransitionName(name, _phoneTransitionName + position);

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