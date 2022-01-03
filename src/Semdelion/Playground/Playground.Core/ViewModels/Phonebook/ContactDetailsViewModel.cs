using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Playground.Core.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Phonebook
{
    public class ContactDetailsViewModel : BaseViewModel<ContactNavParams>
    {
        public string PhotoUri { get; set; } 

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ContactDetailsViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService) 
            : base(loggerFactory, navigationService) { }

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
