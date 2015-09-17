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

//			if (tableItems[indexPath.Row].Done) 
//				cell.Accessory = UITableViewCellAccessory.Checkmark;
//			else
//				cell.Accessory = UITableViewCellAccessory.None;

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

		public Task GetItem(int id) {
			return tableItems[id];
		}
	}
}

