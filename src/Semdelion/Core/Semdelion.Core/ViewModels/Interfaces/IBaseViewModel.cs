using System.ComponentModel;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Semdelion.Core.Enums;

namespace Semdelion.Core.ViewModels.Interfaces
{
    public interface IBaseViewModel: IMvxViewModel, INotifyPropertyChanged
    {
        /// <summary>
        ///     Заголовок ViewModel.
        /// </summary>
        string Title { get; }

        /// <summary>
        ///     Состояние ViewModel.
        /// </summary>
        States State { get; set; }

        /// <summary>
        ///     Сообщение о состояние ViewModel.
        /// </summary>
        string StateMessage { get; set; }

        /// <summary>
        ///     Сообщение о состояние ViewModel.
        /// </summary>
        IMvxCommand RefreshCommand { get; set; }
    }

    /// <summary>
    ///     Базовая вью модель.
    /// </summary>
    /// <typeparam name="TParameter">Тип параметра навигации.</typeparam>
    public interface IBaseViewModel<TParameter> : IBaseViewModel, IMvxViewModel<TParameter>
    {
    }

    /// <summary>
    ///     Базовая вью модель.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата.</typeparam>
    public interface IBaseViewModelResult<TResult> : IBaseViewModel, IMvxViewModelResult<TResult>
    {
    }

    /// <summary>
    ///     Базовая вью модель.
    /// </summary>
    /// <typeparam name="TParameter">Тип параметра навигации.</typeparam>
    /// <typeparam name="TResult">Тип возвращаемого результата.</typeparam>
    public interface IBaseViewModel<TParameter, TResult> : IBaseViewModel, IMvxViewModel<TParameter>, IMvxViewModelResult<TResult>
    {
    }
}
