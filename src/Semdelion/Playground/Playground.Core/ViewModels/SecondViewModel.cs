﻿using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
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
    public class SecondViewModel : BasePageCollectionViewModel<Contact>
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

        protected override async Task<IList<Contact>> LoadOnDemandItems(CancellationToken ct = default)
        {
            if(Items == null)
                State = States.Loading;

            var requestResult = await ContactProvider.GetContacts(_pageSize, _page++);

            this.UpdateState(requestResult);

            //State = States.NoInternet;
            //await Task.Delay(2000);
            //State = States.Error;
            //await Task.Delay(2000);
            //State = States.NoData;
            //await Task.Delay(2000);
            //State = States.Normal;
            //await Task.Delay(2000);
            //State = States.NoInternet;
            //await Task.Delay(2000);
            //State = States.Error;
            //await Task.Delay(2000);
            //State = States.NoData;

            return requestResult?.Data?.Contacts;
        }

        protected override Task DoRefreshCommand()
        {
            _page = 1;
            Items.Clear();
            return base.DoRefreshCommand();
        }


        protected override async Task DoItemClickCommand(Contact item)
        {
            await NavigationService.Navigate<ContactDetailsViewModel, ContactNavParams>( new ContactNavParams(item));
        }
    }
}
