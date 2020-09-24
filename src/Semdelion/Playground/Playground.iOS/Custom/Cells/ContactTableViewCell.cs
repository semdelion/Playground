using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Semdelion.API.Models;
using UIKit;

namespace Playground.iOS.Custom.Cells
{
    public partial class ContactTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ContactTableViewCell");
        public static readonly UINib Nib;

        static ContactTableViewCell()
        {
            Nib = UINib.FromName("ContactTableViewCell", NSBundle.MainBundle);
        }

        protected ContactTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ContactTableViewCell, Contact>();
                set.Bind(Image).For(v => v.ImagePath).To(vm => vm.Photo.Large);
                set.Bind(NameLabel).To(vm => vm.Name.First);
                set.Apply();
            });
        }
    }
}
