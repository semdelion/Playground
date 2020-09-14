using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
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
    public class SecondViewModel : BaseCollectionViewModel<Contact>
    {
        protected IContactProvider ContactProvider { get; }
        public int _page = 1;
        public int _pageSize = 5;

        public override string Title => "SecondViewModel";

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IContactProvider contactProvider)
           : base(logProvider, navigationService)
        {
            ContactProvider = contactProvider;
        }

        protected override async Task<IList<Contact>> LoadOnDemandItems(CancellationToken ct = default)
        {
            State = States.Loading;
            var requestResult = await ContactProvider.GetContacts(_pageSize, _page++);

            this.UpdateState(requestResult);

            return requestResult.Data.Contacts;
        }

        protected override async Task DoItemClickCommand(Contact item)
        {
            
        }
    }
}
