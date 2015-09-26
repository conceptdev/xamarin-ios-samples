using System;
using UIKit;

namespace StoryboardTables
{
	public class AllCollectionSource : UICollectionViewSource
	{
		public AllCollectionSource ()
		{
		}

		Task[] tableItems;
		string cellIdentifier = "todocell";

		public AllCollectionSource (Task[] items)
		{
			tableItems = items; 
		}
		public override UICollectionViewCell GetCell (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell
			var cell = collectionView.DequeueReusableCell (cellIdentifier, indexPath) as AllViewCell;

			cell.Name.Text = tableItems[indexPath.Row].Name;

			if (tableItems [indexPath.Row].Done)
				cell.Done.Image = UIImage.FromBundle ("checkbox");
			else
				cell.Done.Image = UIImage.FromBundle ("box");

			return cell;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return tableItems.Length;
		}

		public override bool ShouldHighlightItem (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
//			var cell = collectionView.CellForItem (indexPath);
//			cell.ContentView.BackgroundColor = UIColor.LightGray;
			return true;
		}

		public override void ItemHighlighted (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.FromRGB(0x34,0x98,0xDB).ColorWithAlpha(0x88);
		}
		public override void ItemUnhighlighted (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.White;
		}

		public Task GetItem(int id) {
			return tableItems[id];
		}
	}
}

