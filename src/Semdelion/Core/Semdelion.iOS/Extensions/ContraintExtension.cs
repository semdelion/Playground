namespace Semdelion.iOS.Extensions
{
    using UIKit;

    /// <summary>
    ///     Расширение для работы с constraints.
    /// </summary>
    public static class ContraintExtension
    {
        public static NSLayoutConstraint[] SetFillYContraintTo(this UIView childView, UIView superView, float margin = 0f)
        {
            if (childView.Superview == superView)
            {
                var str = $"V:|-{margin}-[childView]-{margin}-|";
                var cs = NSLayoutConstraint.FromVisualFormat(str, 0, "childView", childView);
                superView.AddConstraints(cs);

                return cs;
            }

            return null;
        }

        public static NSLayoutConstraint[] SetFillXContraintTo(this UIView childView, UIView superView, float margin = 0f)
        {
            if (childView.Superview == superView)
            {
                var str = $"H:|-{margin}-[childView]-{margin}-|";
                var cs = NSLayoutConstraint.FromVisualFormat(str, 0, "childView", childView);
                superView.AddConstraints(cs);

                return cs;
            }

            return null;
        }

        public static void SetBottomContraintTo(this UIView childView, UIView superView, float margin = 0, NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            superView.AddConstraint(NSLayoutConstraint.Create(
                superView,
                NSLayoutAttribute.Bottom,
                relation,
                childView,
                NSLayoutAttribute.Bottom,
                1,
                margin));
        }

        public static void SetLeftContraintTo(this UIView childView, UIView superView, float margin = 0, NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            superView.AddConstraint(NSLayoutConstraint.Create(
                childView,
                NSLayoutAttribute.Left,
                relation,
                superView,
                NSLayoutAttribute.Left,
                1,
                margin));
        }

        public static void SetRightContraintTo(this UIView childView, UIView superView, float margin = 0, NSLayoutRelation relation = NSLayoutRelation.Equal)
        {
            superView.AddConstraint(NSLayoutConstraint.Create(
                superView,
                NSLayoutAttribute.Right,
                relation,
                childView,
                NSLayoutAttribute.Right,
                1,
                margin));
        }
    }
}
