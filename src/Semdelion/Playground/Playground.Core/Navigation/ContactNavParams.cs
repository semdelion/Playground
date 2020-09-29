using Semdelion.API.Models;

namespace Playground.Core.Navigation
{
    public class ContactNavParams
    {
        public ContactNavParams(Contact contact)
        {
            ContactModel = contact;
        }

        public Contact ContactModel { get; set; }
    }
}
