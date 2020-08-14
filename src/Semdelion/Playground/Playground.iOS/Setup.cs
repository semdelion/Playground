using Playground.Core;
using Semdelion.Core;
using Semdelion.iOS;

namespace Playground.iOS
{
    public class Setup : BaseIosSetup
    {
        protected override App CreateSemApp() => new PlaygroundApp();
    }
}
