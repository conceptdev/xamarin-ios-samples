using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace WatchTodo
{
	public class TodoItemSource : UITableViewSource
	{
		string cellIdentifier = "todoItemCell";
		List<TodoItem> tableItems;

		public TodoItemSource (List<TodoItem> items)
		{
			this.tableItems = items;
		}
		public TodoItem GetItem (int index)
		{
			return tableItems[index];
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("Row " + indexPath.Row.ToString () + " deselected");
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			var item = tableItems [indexPath.Row];

			cell.TextLabel.Text = item.Name;
			cell.Accessory = item.Done ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}
	}
}

