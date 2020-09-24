using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels
{
    public class ContactDetailsViewModel : BaseViewModel<ContactNavParams>
    {
        public string Photo { get; set; } 

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ContactDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }

        public override void Prepare(ContactNavParams parameter)
        {
            Photo = parameter.ContactModel.Photo.Large;
            FullName = parameter.ContactModel.Name.Last + " " + parameter.ContactModel.Name.First;
            Phone = parameter.ContactModel.Phone;
            Email = parameter.ContactModel.Email;
        }
    }
}
