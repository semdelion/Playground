using Playground.Core.ViewModels.Phonebook.CellElements;

namespace Playground.Core.Navigation
{
    public class ContactNavParams
    {
        public ContactNavParams(ContactCellElement contact)
        {
            ContactModel = contact;
        }

        public ContactCellElement ContactModel { get; set; }
    }
}
