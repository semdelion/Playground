using MvvmCross.ViewModels;

namespace Semdelion.Core.ViewModels.Interfaces
{
    public interface IBaseCollectionViewModel<TItem> : IBaseViewModel
    {
        /// <summary>
        ///     Список элементов.
        /// </summary>
        MvxObservableCollection<TItem> Items { get; }
    }
}
