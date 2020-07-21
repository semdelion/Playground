using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Playground.Core.ViewModels;
using System.Threading.Tasks;

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

            RegisterAppStart<RootViewModel>();
        }
    }
}
