using Playground.Core.ViewModels;
using Semdelion.Core;

namespace Playground.Core
{
    public class PlaygroundApp: App 
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<MainViewModel>();
        }
    }
}
