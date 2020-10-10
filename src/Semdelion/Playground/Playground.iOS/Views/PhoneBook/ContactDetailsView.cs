using Playground.Core.ViewModels;
using Semdelion.iOS.Views.Base;
using MvvmCross.Binding.BindingContext;

namespace Playground.iOS.Views.PhoneBook
{
    public partial class ContactDetailsView : BaseViewController<ContactDetailsViewModel> 
    {
        public ContactDetailsView() : base(nameof(ContactDetailsView), null)
        {
        }

        protected override void Binding()
        {
            base.Binding();

            var set = this.CreateBindingSet<ContactDetailsView, ContactDetailsViewModel>();
            set.Bind(PhotoImageView).For(v => v.ImagePath).To(vm => vm.PhotoUri);
            set.Bind(FullNameLabel).To(vm => vm.FullName);
            set.Bind(PhoneLabel).To(vm => vm.Phone);
            set.Bind(EmailLabel).To(vm => vm.Email);
            set.Apply();
        }
    }
}

