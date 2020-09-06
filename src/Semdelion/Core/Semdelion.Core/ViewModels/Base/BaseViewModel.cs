namespace Semdelion.Core.ViewModels.Base
{
    using MvvmCross.Commands;
    using MvvmCross.Localization;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Semdelion.Core.Enums;
    using Semdelion.Core.ViewModels.Interfaces;

    public abstract class BaseViewModel : MvxNavigationViewModel, IBaseViewModel, IMvxLocalizedTextSourceOwner
    {
        #region Fields

        private IMvxLanguageBinder _localizedTextSource;
        private States _state;

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

        public virtual IMvxLanguageBinder LocalizedTextSource => _localizedTextSource ??= new MvxLanguageBinder("");

        #endregion

        #region Constructor

        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        #endregion

        #region Private
        #endregion
    }
}
