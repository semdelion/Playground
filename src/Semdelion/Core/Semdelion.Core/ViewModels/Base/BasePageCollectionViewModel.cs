using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Interfaces;
using System.Collections.Generic;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BasePageCollectionViewModel<TItem> : BaseCollectionViewModel<TItem>, IBasePageCollectionViewModel<TItem>
    {
        protected int CurrentCount => IsRefreshing ? 0 : (Items?.Count ?? 0);

        protected BasePageCollectionViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) 
            : base(logProvider, navigationService) { }

        protected override void SetItems(IList<TItem> items)
        {
            if (Items == null)
                base.SetItems(items);
            else if (items != null)
                Items.AddRange(items);
        }
    }
}
