using Playground.Core.CellElements;
using Semdelion.API.Models;

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
