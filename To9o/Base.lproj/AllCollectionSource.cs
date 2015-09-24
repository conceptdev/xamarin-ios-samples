using System;
using UIKit;

namespace StoryboardTables
{
	public class AllCollectionSource : UICollectionViewSource
	{
		public AllCollectionSource ()
		{
		}

		// ##
		// there is NO database or storage of Tasks in this example, just an in-memory List<>
		// refer to the other Tasky samples on github for an implementation using SQLite-NET
		// ##
		Task[] tableItems;
		string cellIdentifier = "todocell";

		public AllCollectionSource (Task[] items)
		{
			tableItems = items; 
		}
		public override UICollectionViewCell GetCell (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
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
		public override nint NumberOfSections (UICollectionView collectionView)
		{
			return 1;
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
			cell.ContentView.BackgroundColor = UIColor.LightGray;
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

