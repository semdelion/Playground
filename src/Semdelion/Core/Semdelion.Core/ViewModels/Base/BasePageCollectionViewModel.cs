using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Interfaces;
using System.Collections.Generic;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BasePageCollectionViewModel<TItem> : BaseCollectionViewModel<TItem>, IBasePageCollectionViewModel<TItem>
    {
        protected int CurrentCount => IsRefreshing ? 0 : (Items?.Count ?? 0);

        protected BasePageCollectionViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService) 
            : base(loggerFactory, navigationService) { }

        protected override void SetItems(IList<TItem> items)
        {
            if (Items == null)
                base.SetItems(items);
            else if (items != null)
                Items.AddRange(items);
        }
    }
}
