using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.Core.Enums;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Commands;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseViewModel<TParameter, TResult> : MvxNavigationViewModel<TParameter, TResult>, IBaseViewModel<TParameter, TResult>
    {
        public virtual string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; } = null;

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {

        }
    }

    /// <summary>
    ///     Базовая вью модель, которая на вход принимает параметр.
    /// </summary>
    /// <typeparam name="TParameter">Тип параметра навигации.</typeparam>
    public abstract class BaseViewModel<TParameter> : MvxNavigationViewModel<TParameter>, IBaseViewModel<TParameter>
        where TParameter : class
    {
        public string Title => string.Empty;

        public States State { get; set; }
        public IMvxCommand RefreshCommand { get; set; } = null;
        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService)
        {

        }
    }
    /// <summary>
    ///     Базовая вью модель, которая возвращает только результат при закрытии текущей вью модели.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата.</typeparam>
    public abstract class BaseViewModelResult<TResult> : MvxViewModelResult<TResult>, IBaseViewModelResult<TResult>
        where TResult : class
    {
        public string Title => string.Empty;

        public States State { get; set; }
        public IMvxCommand RefreshCommand { get; set; }

    }
}