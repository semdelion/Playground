using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BasePageCollectionViewModel<TItem> : BaseCollectionViewModel<TItem>, IBasePageCollectionViewModel<TItem>
    {
        /// <inheritdoc />
        public abstract int? TotalCount { get; set; }

        protected int CurrentCount => IsRefreshing ? 0 : (Items?.Count ?? 0);

        protected BasePageCollectionViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        protected override Task DoRefreshCommand()
        {
            TotalCount = null;
            return base.DoRefreshCommand();
        }
    }
}
