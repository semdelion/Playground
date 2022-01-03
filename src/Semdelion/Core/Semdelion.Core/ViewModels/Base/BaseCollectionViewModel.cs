using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseCollectionViewModel<TItem>: BaseViewModel, IBaseCollectionViewModel<TItem>
    {
        #region Fields
        private MvxObservableCollection<TItem> _items;
        private IMvxCommand _itemClickCommand;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRefreshing;
        #endregion

        #region Commands
        public virtual IMvxCommand ItemClickCommand
        {
            get => _itemClickCommand ??= new MvxAsyncCommand<TItem>(DoItemClickCommand);
            set => _itemClickCommand = value;
        }

        /// <inheritdoc />
        public override IMvxCommand RefreshCommand => new MvxAsyncCommand(DoRefreshCommand, () => !IsRefreshing);
        #endregion

        #region Properties
        /// <inheritdoc />
        public virtual MvxObservableCollection<TItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        #endregion

        #region Constructors
        /// <inheritdoc />
        protected BaseCollectionViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService)
            : base(loggerFactory, navigationService) { }
        #endregion

        #region Protected
        protected abstract Task<IList<TItem>> LoadOnDemandItems(CancellationToken ct = default);

        protected virtual async Task DoItemClickCommand(TItem item) { }

        protected virtual async Task DoRefreshCommand()
        {
            IsRefreshing = true;
            await ReloadItems(CreateCancellationToken());
            IsRefreshing = false;
        }

        protected async void LoadItemsInBackground()
        {
            await ReloadItems(CreateCancellationToken());
        }

        protected virtual async Task ReloadItems(CancellationToken cancellationToken)
        {
            var items = await LoadOnDemandItems(cancellationToken);

            SetItems(items);
        }

        protected virtual void SetItems(IList<TItem> newItems)
        {
            Items = newItems == null
                ? new MvxObservableCollection<TItem>()
                : new MvxObservableCollection<TItem>(newItems);
        }

        protected virtual CancellationToken CreateCancellationToken()
        {
            DestroyCancellationToken();
            _cancellationTokenSource = ApiMethodContext.CancellationTokenSourceFunc();
            return _cancellationTokenSource.Token;
        }

        protected virtual void DestroyCancellationToken()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
        #endregion

        #region Public
        /// <inheritdoc />
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            LoadItemsInBackground();
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
            DestroyCancellationToken();
        }
        #endregion
    }
}
