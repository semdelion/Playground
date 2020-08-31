using System.Linq;
using Playground.iOS.Custom.ScrollablePage;
using UIKit;

namespace Playground.iOS.Custom.Pages
{
    public class PageViewController : ScrollablePageViewController
    {
        public PageViewController(IPageViewControllerDataSource dataSource) : base(dataSource)
        {
            SetViewControllers(
                new[] { dataSource.ViewControllers.FirstOrDefault() as UIViewController },
                UIPageViewControllerNavigationDirection.Reverse,
                false,
                null);

            View.BackgroundColor = UIColor.Clear;
        }
    }
}
