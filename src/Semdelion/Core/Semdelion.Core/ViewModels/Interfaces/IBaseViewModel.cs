using MvvmCross.Localization;
using MvvmCross.ViewModels;
using Semdelion.Core.Enums;
using System.ComponentModel;

namespace Semdelion.Core.ViewModels.Interfaces
{
    public interface IBaseViewModel: IMvxViewModel, INotifyPropertyChanged
    { 
        States State { get; set; }
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
