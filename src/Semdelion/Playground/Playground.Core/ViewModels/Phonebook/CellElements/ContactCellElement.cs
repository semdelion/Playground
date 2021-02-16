using Semdelion.API.Models;

namespace Playground.Core.ViewModels.Phonebook.CellElements
{
    public class ContactCellElement
    {
        protected Contact Model { get; }
        public ContactCellElement(Contact contact)
        {
            Model = contact;
        }

        public string PhotoUri => Model.Photo.Large;

        public string Email => Model.Email;

        public string FullName => $"{Model.Name.Last} {Model.Name.First}";

        public string Phone => Model.Phone;
    }
}
