using Playground.Core;
using Semdelion.Core;
using Semdelion.Droid;

namespace Playground.Droid
{
    public class Setup : BaseDroidSetup
    {
        protected override App CreateSemApp() => new PlaygroundApp();
    }
}