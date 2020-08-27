using MvvmCross;
using Playground.Core.Services;
using Playground.Core.ViewModels;
using Semdelion.Core;
using Semdelion.DAL.Services.Decorators;

namespace Playground.Core
{
    public class PlaygroundApp: App 
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.RegisterType<IContactService, ContactService>();

            var serviceDecorator = new ServiceDecorator();
            ServiceDecorator.AddDefaultFilters(serviceDecorator);

            Mvx.IoCProvider.RegisterSingleton<IServiceDecorator>(serviceDecorator);


            RegisterAppStart<MainViewModel>();
        }
    }
}
