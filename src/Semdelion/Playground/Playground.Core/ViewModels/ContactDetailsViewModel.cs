using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels
{
    public class ContactDetailsViewModel : BaseViewModel<ContactNavParams>
    {
        public string PhotoUri { get; set; } 

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ContactDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) 
            : base(logProvider, navigationService) { }

        public override void Prepare(ContactNavParams parameter)
        {
            if (parameter == null || parameter.ContactModel == null)
                return;
            PhotoUri = parameter.ContactModel.PhotoUri;
            FullName = parameter.ContactModel.FullName;
            Phone = parameter.ContactModel.Phone;
            Email = parameter.ContactModel.Email;
        }
    }
}
