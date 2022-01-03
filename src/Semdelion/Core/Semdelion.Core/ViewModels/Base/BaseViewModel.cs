namespace Semdelion.Core.ViewModels.Base
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Localization;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Semdelion.Core.Enums;
    using Semdelion.Core.ViewModels.Interfaces;

    public abstract class BaseViewModel : MvxNavigationViewModel, IBaseViewModel, IMvxLocalizedTextSourceOwner, ICancelViewModel
    {
        #region Fields
        public string Key => GetType().Name;

        private IMvxLanguageBinder _localizedTextSource;
        private States _state;
        #endregion

        #region Commands

        /// <inheritdoc cref="ICancelViewModel"/>
        public virtual IMvxCommand CancelCommand => new MvxAsyncCommand(() => NavigationService.Close(this));
        #endregion

        #region Properties
        public virtual string Title { get; set; } = string.Empty;

        public States State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public virtual IMvxCommand RefreshCommand { get; set; } = null;

        public string this[string localizeKey] => LocalizedTextSource.GetText(localizeKey);
        #endregion

        #region Services
        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder(string.Empty);
        #endregion

        #region Constructor
        protected BaseViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService)
            : base(loggerFactory, navigationService) { }
        #endregion
    }
}
