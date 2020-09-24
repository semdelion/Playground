namespace Semdelion.iOS.Views.States
{
    using System.Linq;
    using Airbnb.Lottie;
    using CoreGraphics;
    using Foundation;
    using MvvmCross.Commands;
    using Semdelion.Core.Helpers;
    using Semdelion.iOS.Extensions;
    using UIKit;

    public partial class NoInternetView : UIView
    {
        public UIView ContentView { get; private set; }

        public override CGSize IntrinsicContentSize => base.IntrinsicContentSize;
        IMvxCommand RefreshCommand { get; set; }
        public NoInternetView(CGRect frame, IMvxCommand command) : base(frame)
        {
            RefreshCommand = command;
            CommonInit();
        }

        private UIView ViewFromNib()
        {
            NSBundle.MainBundle.LoadNib(nameof(NoInternetView), this, null);
            var nib = UINib.FromName(nameof(NoInternetView), NSBundle.MainBundle);
            var view = nib.Instantiate(this, null).First() as UIView;
            return view;
        }

        private void CommonInit()
        {
            ContentView = ViewFromNib();
            ContentView.Frame = Frame;

            NoInternetLabel.Text = Localize.GetText("State.NoInternet.Message");
            UpdateButton.TouchUpInside += (sender, e) => {
                RefreshCommand.Execute();
            };
            AddSubview(ContentView);

            ContentView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            ContentView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            ContentView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
            ContentView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;

            LOTAnimationView lottie = LOTAnimationView.AnimationNamed("no-internet-connection");
            lottie.ContentMode = UIViewContentMode.ScaleAspectFit;
            LottieView.Frame = new CGRect(LottieView.Frame.X, LottieView.Frame.Y, Frame.Width, Frame.Width);
          
            LottieView.AddSubview(lottie);
            lottie.Frame = LottieView.Bounds;
            lottie.SetFillXContraintTo(LottieView);
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