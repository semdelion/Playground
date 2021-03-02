using System;
using Foundation;
using UIKit;

namespace Playground.iOS.Views.Settings.Cells
{
    public partial class FiltersCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(FiltersCollectionViewCell));
        public static readonly UINib Nib;

        static FiltersCollectionViewCell()
        {
            Nib = UINib.FromName(nameof(FiltersCollectionViewCell), NSBundle.MainBundle);
        }

        protected FiltersCollectionViewCell(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Layer.BorderWidth = 1;
            UpdateTheme(true);
        }

        public void UpdateTheme(bool selected)
        {
            var color = selected ? UIColor.SystemBlueColor : UIColor.White;
            Layer.BorderColor = color.CGColor;
            FilterLabel.TextColor = color;
            Layer.CornerRadius = 10;
        }
    }
}
