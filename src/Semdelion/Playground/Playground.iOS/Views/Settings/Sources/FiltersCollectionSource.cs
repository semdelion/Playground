namespace Loymax.Module.ClientSettings.iOS.Custom.CollectionSource
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Foundation;
    using Playground.Core.ViewModels.Settings;
    using Playground.iOS.Views.Settings.Cells;
    using UIKit;

    public class FiltersCollectionSource : UICollectionViewSource
    {
        private readonly List<string> Items;
        private readonly LogsViewModel ViewModel;

        public FiltersCollectionSource(LogsViewModel logsViewModel)
        {
            Items = new List<string> { "Fatal", "Error", "Warn", "Info", "Trace", "Debug" };
            ViewModel = logsViewModel;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
            => Items.Count();

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(FiltersCollectionViewCell.Key, indexPath) as FiltersCollectionViewCell;
            cell.FilterLabel.Text = Items[indexPath.Row];
            return cell;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
            => SetFilter(collectionView, indexPath, false);

        public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
            => SetFilter(collectionView, indexPath, true);

        public void SetFilter(UICollectionView collectionView, NSIndexPath indexPath, bool selected)
        {
            var cell = (FiltersCollectionViewCell)collectionView.CellForItem(indexPath);
            cell.UpdateTheme(selected);

            switch (Items[indexPath.Row])
            {
                case "Fatal":
                    ViewModel.Fatal = selected;
                    break;
                case "Error":
                    ViewModel.Error = selected;
                    break;
                case "Warn":
                    ViewModel.Warn = selected;
                    break;
                case "Info":
                    ViewModel.Info = selected;
                    break;
                case "Trace":
                    ViewModel.Trace = selected;
                    break;
                case "Debug":
                    ViewModel.Debug = selected;
                    break;
            }
        }
    }
}
