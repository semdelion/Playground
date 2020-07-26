using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels;
namespace Playground.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

           // Mvx.IoCProvider.RegisterSingleton<IMvxTextProvider>(new TextProviderBuilder().TextProvider);

            RegisterAppStart<MainViewModel>();
        }
    }
}
