using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Todo11App
{
    public partial class TableViewController : UITableViewController, IUITableViewDataSource, IUITableViewDelegate
    {
		/// <summary>List of items</summary>
		List<TodoItem> todoItems;

        public TableViewController (IntPtr handle) : base (handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Todo 11", ""); 
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Specify the table as its own drag source and drop delegate
			//this.TableView.DragDelegate = this;
			//this.TableView.DropDelegate = this;

			// Impelement delegate and datasource for tableview to operate
			TableView.DataSource = this;
			TableView.Delegate = this;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			todoItems = AppDelegate.Current.TodoMgr.GetOrderedTodos().ToList(); //ordered for CollectionView

            // bind every time, to reflect deletion in the Detail view
            TableView.ReloadData();
		}

		/// <summary>
		/// Prepares for segue.
		/// </summary>
		/// <remarks>
		/// The prepareForSegue method is invoked whenever a segue is about to take place. 
		/// The new view controller has been loaded from the storyboard at this point but 
		/// it’s not visible yet, and we can use this opportunity to send data to it.
		/// </remarks>
		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "todosegue")
			{ // set in Storyboard
				var tvc = segue.DestinationViewController as DetailViewController;
				if (tvc != null)
				{
                    var source = TableView.DataSource as TableViewController;
                    var rowPath = TableView.IndexPathForCell(sender as UITableViewCell);
					var item = source.GetItem(rowPath.Row);
					//tvc.Delegate = this;
					tvc.SetTodo(item);
				}
			}
		}

		public TodoItem GetItem(int id)
		{
			return todoItems[id];
		}

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
            //HACK: todoItems.MoveItem(sourceIndexPath.Row, destinationIndexPath.Row);
		}
    }
}