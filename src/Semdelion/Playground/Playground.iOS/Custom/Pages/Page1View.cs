using Playground.iOS.Custom.ScrollablePage;
using UIKit;

namespace Playground.iOS.Custom.Pages
{
    public partial class Page1View : UIViewController, IPageViewControllerIndex
    {
        public int Index { get; set; }

        public Page1View() : base(nameof(Page1View), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

