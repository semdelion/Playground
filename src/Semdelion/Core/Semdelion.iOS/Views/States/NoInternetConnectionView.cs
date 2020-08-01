namespace Semdelion.iOS.Views.States
{
    using Airbnb.Lottie;
    using CoreGraphics;
    using Semdelion.iOS.Extensions;
    using UIKit;

    public class NoInternetConnectionView : UIView
    {
        public NoInternetConnectionView(CGRect frame) : base(frame)
        {
            LOTAnimationView lottie = LOTAnimationView.AnimationNamed("no-internet-connection");
            lottie.ContentMode = UIViewContentMode.ScaleAspectFit;
            lottie.Frame = frame;
            AddSubview(lottie);
            lottie.SetFillXContraintTo(this);
            lottie.SetBottomContraintTo(this, 0, NSLayoutRelation.GreaterThanOrEqual);
            AddConstraint(NSLayoutConstraint.Create(lottie, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, 0));
            lottie.Play();
        }
    }
}
