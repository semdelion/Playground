namespace Semdelion.iOS.Views.States
{
    using System.Linq;
    using CoreGraphics;
    using Foundation;
    using UIKit;

    public partial class LoadingView : UIView
    {
        public UIView ContentView { get; private set; }

        public override CGSize IntrinsicContentSize => base.IntrinsicContentSize;

        public LoadingView(CGRect frame) : base(frame)
        {
            CommonInit();
        }

        private UIView ViewFromNib()
        {
            NSBundle.MainBundle.LoadNib("LoadingView", this, null);
            var nib = UINib.FromName("LoadingView", NSBundle.MainBundle);
            var view = nib.Instantiate(this, null).First() as UIView;
            return view;
        }

        private void CommonInit()
        {
            ContentView = ViewFromNib();
            ContentView.Frame = Frame;
            ContentView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight & UIViewAutoresizing.FlexibleWidth;
            AutoresizingMask = UIViewAutoresizing.FlexibleHeight & UIViewAutoresizing.FlexibleWidth;

            AddSubview(ContentView);
            TranslatesAutoresizingMaskIntoConstraints = false;
            ContentView.TranslatesAutoresizingMaskIntoConstraints = false;

            ContentView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            ContentView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            ContentView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
            ContentView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            AddConstraint(NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, 0));
            Loader.StartAnimating();
        }

        public override void MovedToWindow()
        {
            base.MovedToWindow();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0) && Window != null)
                BottomAnchor.ConstraintLessThanOrEqualToSystemSpacingBelowAnchor(Window.BottomAnchor, 1).Active = true;
        }
    }
}