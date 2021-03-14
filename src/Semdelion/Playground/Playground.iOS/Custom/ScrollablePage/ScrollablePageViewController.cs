using UIKit;

namespace Playground.iOS.Custom.ScrollablePage
{
    public class ScrollablePageViewController : UIPageViewController
    {
        public ScrollablePageViewController(IPageViewControllerDataSource dataSource) :
            base(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal)
        {
            DataSource = PageDataSource = dataSource;
        }

        protected IPageViewControllerDataSource PageDataSource { get; }

        public void SetNextItem()
        {
            SetViewControllers(
                new[] { DataSource.GetNextViewController(this, PageDataSource.CurrentViewController) },
                UIPageViewControllerNavigationDirection.Forward,
                true,
                null);
        }

        public void SetPreviousItem()
        {
            SetViewControllers(
                new[] { DataSource.GetPreviousViewController(this, PageDataSource.CurrentViewController) },
                UIPageViewControllerNavigationDirection.Reverse,
                true,
                null);
        }
    }
}
