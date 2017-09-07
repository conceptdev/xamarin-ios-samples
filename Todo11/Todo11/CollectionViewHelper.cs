using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace To11oApp
{
//	public class CollectionLayout : UICollectionViewDelegateFlowLayout {
//		public override UIEdgeInsets GetInsetForSection (UICollectionView collectionView, UICollectionViewLayout layout, nint section)
//		{
//			return new UIEdgeInsets (0, 0, 0, 0);
//		}
//		public override CoreGraphics.CGSize GetSizeForItem (UICollectionView collectionView, UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
//		{
//			return new CoreGraphics.CGSize (collectionView.Bounds.Width, 44);
//		}
//	}



	public class TodoCollectionSource : UICollectionViewSource
	{ 
		public TodoCollectionSource ()
		{
		}

		List<TodoItem> tableItems;
		string cellIdentifier = "todocell";

		public TodoCollectionSource (TodoItem[] items)
		{
			tableItems = items.ToList(); 
		}
		public override UICollectionViewCell GetCell (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell
			var cell = collectionView.DequeueReusableCell (cellIdentifier, indexPath) as TodoCell;

			cell.SetData (tableItems [indexPath.Row]);

			//Console.WriteLine (" *** " + indexPath.Row);
			//Console.WriteLine (" --- " + tableItems [indexPath.Row].Order);
			tableItems [indexPath.Row].Order = indexPath.Row;
			//Console.WriteLine (" --- " + tableItems [indexPath.Row].Order);

			return cell;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return tableItems.Count;
		}

		public override void ItemHighlighted (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.FromRGB(0x34,0x98,0xDB).ColorWithAlpha(0xcc);
		}
		public override void ItemUnhighlighted (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.White;
		}

		public TodoItem GetItem(int id) {
			return tableItems[id];
		}

		#region iOS 9 Collection View improvements
		public override bool CanMoveItem (UICollectionView collectionView, NSIndexPath indexPath)
		{
			return true;
		}
		public override void MoveItem (UICollectionView collectionView, NSIndexPath sourceIndexPath, Foundation.NSIndexPath destinationIndexPath)
		{
			var item = tableItems [(int)sourceIndexPath.Item];
			Console.WriteLine("move item " + item.Name + $" from {sourceIndexPath.Row} to {destinationIndexPath.Row}");

			// set the listorder
			tableItems.RemoveAt ((int)sourceIndexPath.Item);
			tableItems.Insert ((int)destinationIndexPath.Item, item);

			// update the database to match (brute force method)
			AppDelegate.Current.TodoMgr.Reorder (tableItems);
		}
		#endregion
	}
}

