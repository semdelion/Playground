using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Playground.Core.Navigation;
using Playground.Core.Providers;
using Playground.Core.ViewModels.Phonebook.CellElements;
using Semdelion.API.Models;
using Semdelion.Core.Enums;
using Semdelion.Core.Extensions;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Phonebook
{
    public class ContactsViewModel : BasePageCollectionViewModel<ContactCellElement>
    {
        protected IContactProvider ContactProvider { get; }
        public int _page = 1;
        public int _pageSize = 10;

        public override string Title => this["ContactsViewModel.Title"];

        public ContactsViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService, IContactProvider contactProvider)
           : base(loggerFactory, navigationService)
        {
            ContactProvider = contactProvider;
        }

        public IMvxAsyncCommand LoadNextPage => new MvxAsyncCommand(ReloadItems);

        protected override async Task<IList<ContactCellElement>> LoadOnDemandItems(CancellationToken ct = default)
        {
            if(Items == null)
                State = States.Loading;

            var requestResult = await ContactProvider.GetContacts(_pageSize, _page++);

            this.UpdateState(requestResult);
            var items = new List<ContactCellElement>();
            foreach (var item in requestResult?.Data?.Contacts ?? new List<Contact>())
                items.Add(new ContactCellElement(item));

            return items;
        }

        protected override Task DoRefreshCommand()
        {
            _page = 1;
            Items.Clear();
            return base.DoRefreshCommand();
        }

        protected override async Task DoItemClickCommand(ContactCellElement item)
        {
            await NavigationService.Navigate<ContactDetailsViewModel, ContactNavParams>(new ContactNavParams(item));
        }
    }
}
