using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.Core.Enums;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using MvvmCross.Localization;
using Microsoft.Extensions.Logging;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseViewModel<TParameter, TResult> : MvxNavigationViewModel<TParameter, TResult>,
        IBaseViewModel<TParameter, TResult>,
        IMvxLocalizedTextSourceOwner, 
        ICancelViewModel
        where TParameter : class
        where TResult : class
    {
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;

        public virtual string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; } = null;

        /// <inheritdoc cref="ICancelViewModel"/>
        public virtual IMvxCommand CancelCommand => new MvxAsyncCommand(() => NavigationService.Close(this, default));

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);

        public BaseViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService)
            : base(loggerFactory, navigationService) { }
    }

    /// <summary>
    ///     Базовая вью модель, которая на вход принимает параметр.
    /// </summary>
    /// <typeparam name="TParameter">Тип параметра навигации.</typeparam>
    public abstract class BaseViewModel<TParameter> : MvxNavigationViewModel<TParameter>, IBaseViewModel<TParameter>, 
        IMvxLocalizedTextSourceOwner,
        ICancelViewModel
        where TParameter : class
    {
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;

        public string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; } = null;

        /// <inheritdoc cref="ICancelViewModel"/>
        public virtual IMvxCommand CancelCommand => new MvxCommand(() => NavigationService.Close(this));

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);

        public BaseViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService) { }
    }
    /// <summary>
    ///     Базовая вью модель, которая возвращает только результат при закрытии текущей вью модели.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата.</typeparam>
    public abstract class BaseViewModelResult<TResult> : MvxNavigationViewModelResult<TResult>, IBaseViewModelResult<TResult>,
        IMvxLocalizedTextSourceOwner,
        ICancelViewModel
        where TResult : class
    {
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;

        public string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; }

        /// <inheritdoc cref="ICancelViewModel" />
        public virtual IMvxCommand CancelCommand => new MvxAsyncCommand(() => NavigationService.Close(this, default));

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);

        public BaseViewModelResult(ILoggerFactory loggerFactory, IMvxNavigationService navigationService)
          : base(loggerFactory, navigationService) { }
    }
}