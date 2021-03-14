using System;
using System.Collections.Generic;
using UIKit;

namespace Playground.iOS.Custom.ScrollablePage
{
    public class ScrollablePageDataSource : UIPageViewControllerDataSource, IPageViewControllerDataSource
    {
        private readonly List<IPageViewControllerIndex> _steps;
        private UITabBar TabBar;
        public int CurrentIndex { get; private set; }
       

        public UIViewController CurrentViewController => ViewControllers[CurrentIndex] as UIViewController;

        public ScrollablePageDataSource(List<IPageViewControllerIndex> steps, UITabBar tabBar)
        {
            if (steps == null || steps.Count == 0)
                throw new ArgumentNullException(nameof(steps));

            TabBar = tabBar;
            _steps = steps;
            for (int i = 0; i < steps.Count; i++)
                _steps[i].Index = i;
        }

        public List<IPageViewControllerIndex> ViewControllers => _steps;

         public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
         {
             if (!(referenceViewController is IPageViewControllerIndex step))
                 return null;

             TabBar.SelectedItem = TabBar.Items[1];
             CurrentIndex = step.Index;
             CurrentIndex++;
             if (CurrentIndex == ViewControllers.Count)
             {
                 CurrentIndex--;
                 return null;
             }

             return ViewControllers[CurrentIndex] as UIViewController;
         }

         public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
         {
             if (!(referenceViewController is IPageViewControllerIndex step))
                 return null;

             TabBar.SelectedItem = TabBar.Items[0];
             CurrentIndex = step.Index;
             if (CurrentIndex == 0)
                 return null;

             CurrentIndex--;

             return ViewControllers[CurrentIndex] as UIViewController;
         }
    }
}
