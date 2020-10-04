using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Playground.Core.CellElements;
using Playground.Core.Navigation;
using Playground.Core.Providers;
using Refit;
using Semdelion.API.Models;
using Semdelion.Core.Enums;
using Semdelion.Core.Extensions;
using Semdelion.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : BasePageCollectionViewModel<ContactCellElement>
    {
        protected IContactProvider ContactProvider { get; }
        public int _page = 1;
        public int _pageSize = 10;

        public override string Title => "SecondViewModel";

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IContactProvider contactProvider)
           : base(logProvider, navigationService)
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
            await NavigationService.Navigate<ContactDetailsViewModel, ContactNavParams>( new ContactNavParams(item));
        }
    }
}
