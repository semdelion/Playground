using System.Collections.Generic;
using UIKit;

namespace Playground.iOS.Custom.ScrollablePage
{
    public interface IPageViewControllerDataSource : IUIPageViewControllerDataSource
    {
        UIViewController CurrentViewController { get; }
        List<IPageViewControllerIndex> ViewControllers { get; }
    }

    public interface IPageViewControllerIndex
    {
        int Index { get; set; }
    }
}
