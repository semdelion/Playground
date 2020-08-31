using Playground.Core.ViewModels.Pager;
using Playground.Core.ViewModels.Tabs;
using Semdelion.Core;

namespace Playground.Core
{
    public class PlaygroundApp: App 
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<PagesRootViewModel>();
        }
    }
}
