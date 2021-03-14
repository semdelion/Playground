using MvvmCross.Commands;

namespace Semdelion.Core.ViewModels.Interfaces
{
    /// <summary>
    ///     Позволяет закрыть текущую вью модель.
    /// </summary>
    public interface ICancelViewModel
    {
        /// <summary>
        ///     Команда отмены.
        /// </summary>
        IMvxCommand CancelCommand { get; }
    }
}
