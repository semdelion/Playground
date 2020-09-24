namespace Semdelion.iOS.Views.States
{
    using System.Linq;
    using Airbnb.Lottie;
    using CoreGraphics;
    using Foundation;
    using Semdelion.Core.Helpers;
    using Semdelion.iOS.Extensions;
    using UIKit;

    public partial class ErrorView : UIView
    {
        public UIView ContentView { get; private set; }

        public override CGSize IntrinsicContentSize => base.IntrinsicContentSize;

        public ErrorView(CGRect frame) : base(frame)
        {
            CommonInit();
        }

        private UIView ViewFromNib()
        {
            NSBundle.MainBundle.LoadNib(nameof(ErrorView), this, null);
            var nib = UINib.FromName(nameof(ErrorView), NSBundle.MainBundle);
            var view = nib.Instantiate(this, null).First() as UIView;
            return view;
        }

        private void CommonInit()
        {
            ContentView = ViewFromNib();

            ErrorLabel.Text = Localize.GetText("State.Error");

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

            LOTAnimationView lottie = LOTAnimationView.AnimationNamed("error");
            lottie.ContentMode = UIViewContentMode.ScaleAspectFit;
            LottieView.Frame = new CGRect(LottieView.Frame.X, LottieView.Frame.Y, Frame.Width, Frame.Width);
            lottie.Frame = LottieView.Frame;
            LottieView.AddSubview(lottie);
            lottie.SetFillXContraintTo(LottieView, 16);
            lottie.SetFillYContraintTo(LottieView, 16);
            lottie.SetBottomContraintTo(LottieView, 0, NSLayoutRelation.GreaterThanOrEqual);
            AddConstraint(NSLayoutConstraint.Create(lottie, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, 0));
            lottie.LoopAnimation = true;
            lottie.Play();
        }

        public override void MovedToWindow()
        {
            base.MovedToWindow();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0) && Window != null)
                BottomAnchor.ConstraintLessThanOrEqualToSystemSpacingBelowAnchor(Window.BottomAnchor, 1).Active = true;
        }
    }
}