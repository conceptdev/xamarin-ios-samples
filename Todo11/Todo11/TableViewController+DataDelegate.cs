using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Todo11App
{
	public partial class TableViewController : IUITableViewDataSource, IUITableViewDelegate
	{
		// IUITableViewDataSource
		public override nint RowsInSection(UIKit.UITableView tableView, nint section)
		{
			return todoItems.Count;
		}

		public override UIKit.UITableViewCell GetCell(UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("todocell", indexPath);
			cell.TextLabel.Text = todoItems[indexPath.Row].Name;
			cell.DetailTextLabel.Text = "";
			cell.Accessory = todoItems[indexPath.Row].Done ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
			return cell;
		}

		// IUITableViewDelegate
		public override bool CanMoveRow(UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return true;
		}

		public override void MoveRow(UIKit.UITableView tableView, Foundation.NSIndexPath sourceIndexPath, Foundation.NSIndexPath destinationIndexPath)
		{
            MoveTodo(todoItems[sourceIndexPath.Row], sourceIndexPath.Row, destinationIndexPath.Row);
		}
	}
}