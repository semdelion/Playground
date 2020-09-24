using System.Windows.Input;
using AndroidX.RecyclerView.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace Playground.Droid.Adapters
{
    public class AdapterContacts : MvxRecyclerAdapter
    {
        public ICommand CommandGetContacts { get; set; }
        public AdapterContacts(IMvxAndroidBindingContext bindingContext) : base(bindingContext) { }

        public override int GetItemViewType(int position) => ItemTemplateSelector.GetItemViewType(GetItem(position));

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            if (position >= ItemCount - 1 && CommandGetContacts.CanExecute(null))
                CommandGetContacts.Execute(null);
        }
    }
}