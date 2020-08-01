namespace Semdelion.iOS.Bindings
{
    using System;
    using CoreGraphics;
    using MvvmCross.Binding.Bindings.Target;
    using Semdelion.Core.Enums;
    using Semdelion.iOS.Extensions;
    using Semdelion.iOS.Views.States;
    using UIKit;

    public class StatesTargetBinding : MvxConvertingTargetBinding
    {
        public const string Key = "StatesTargetBinding";
        
        public StatesTargetBinding(UIView view) : base(view)
        {
        }

        public override Type TargetType => typeof(string);

        protected override void SetValueImpl(object target, object value)
        {
            try
            {
                if (!(target is UIView contentView) || value == null) return;
               
                States state = (States)Enum.Parse(typeof(States), value.ToString());
                UIView stateView = new UIView()
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                foreach (UIView view in contentView.Subviews)
                    view.RemoveFromSuperview();

                contentView.Alpha = 1f;
                var _frame = new CGRect(0, 0, contentView.Frame.Width, contentView.Frame.Height);

                switch (state)
                {
                    case States.Normal:
                        var vertStackView = new UIStackView
                        {
                            TranslatesAutoresizingMaskIntoConstraints = false,
                            Distribution = UIStackViewDistribution.EqualSpacing,
                            Axis = UILayoutConstraintAxis.Vertical,
                            Alignment = UIStackViewAlignment.Fill,
                            Spacing = 8
                        };
                        vertStackView.AddArrangedSubview(new UILabel() {Text = "NoInternet", TextColor = UIColor.Black });
                        vertStackView.AddArrangedSubview(new UIButton(UIButtonType.ContactAdd));

                        stateView.AddSubview(vertStackView);

                        vertStackView.SetFillXContraintTo(stateView);
                        vertStackView.SetBottomContraintTo(stateView, 0, NSLayoutRelation.GreaterThanOrEqual);

                        stateView.AddConstraint(
                            NSLayoutConstraint.Create(vertStackView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, stateView, NSLayoutAttribute.CenterY, 1, 0)
                        );
                        //contentView.AddSubview(new UIButton(UIButtonType.ContactAdd) { Center = contentView.Center });
                        //contentView.AddSubview(new UILabel() { Center = contentView.Center, Text = "Normal", TextColor = UIColor.Black });
                        break;
                    case States.Loading:
                        break;
                    case States.NoData:
                        break;
                    case States.NoInternet:
                        stateView = new NoInternetConnectionView(_frame);
                        break;
                    case States.Error:
                        stateView = new ErrorView(_frame);
                        break;
                }
                contentView.AddSubview(stateView);
                stateView.SetFillYContraintTo(contentView, 16);
                stateView.SetLeftContraintTo(contentView, 16);
                stateView.SetRightContraintTo(contentView, 16);
                contentView.SetNeedsUpdateConstraints();
                contentView.LayoutIfNeeded();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
