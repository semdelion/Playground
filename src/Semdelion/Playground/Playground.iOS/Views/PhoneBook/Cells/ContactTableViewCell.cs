using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Playground.Core.CellElements;
using UIKit;

namespace Playground.iOS.Views.PhoneBook.Cells
{
    public partial class ContactTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ContactTableViewCell));
        public static readonly UINib Nib;

        static ContactTableViewCell()
        {
            Nib = UINib.FromName(nameof(ContactTableViewCell), NSBundle.MainBundle);
        }

        protected ContactTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ContactTableViewCell, ContactCellElement>();
                set.Bind(Image).For(v => v.ImagePath).To(vm => vm.PhotoUri);
                set.Bind(NameLabel).To(vm => vm.FullName);
                set.Apply();
            });
        }
    }
}
