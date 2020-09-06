using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.Providers;
using Semdelion.Core.Enums;
using Semdelion.Core.Extensions;
using Semdelion.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        protected IContactProvider ContactProvider { get; }
        public override string Title => "SecondViewModel";

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IContactProvider contactProvider)
           : base(logProvider, navigationService)
        {
            ContactProvider = contactProvider;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
           
            State = States.Loading;
            var t = await ContactProvider.GetContacts(int.MaxValue, 10);
            State = t.IsValid ? (t.Data == null ? States.NoData : States.Normal): StatesExtension.GetState(t.Exception); //TODO t.Data == null ?
        }
    }
}
