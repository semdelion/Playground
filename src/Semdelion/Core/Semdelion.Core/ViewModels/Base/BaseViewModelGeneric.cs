using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.Core.Enums;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using MvvmCross.Localization;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseViewModel<TParameter, TResult> : MvxNavigationViewModel<TParameter, TResult>, IBaseViewModel<TParameter, TResult>, IMvxLocalizedTextSourceOwner 
        where TParameter : class
        where TResult : class
    {
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;

        public virtual string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; } = null;

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService) { }
    }

    /// <summary>
    ///     Базовая вью модель, которая на вход принимает параметр.
    /// </summary>
    /// <typeparam name="TParameter">Тип параметра навигации.</typeparam>
    public abstract class BaseViewModel<TParameter> : MvxNavigationViewModel<TParameter>, IBaseViewModel<TParameter>, IMvxLocalizedTextSourceOwner
        where TParameter : class
    {
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;

        public string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; } = null;

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService) { }
    }
    /// <summary>
    ///     Базовая вью модель, которая возвращает только результат при закрытии текущей вью модели.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата.</typeparam>
    public abstract class BaseViewModelResult<TResult> : MvxViewModelResult<TResult>, IBaseViewModelResult<TResult>, IMvxLocalizedTextSourceOwner
        where TResult : class
    {
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;

        public string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; }

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);
    }
}