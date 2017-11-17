using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace Todo11App
{
    /// <summary>
    /// Implements the delegate methods for consuming data for a drop interaction
    /// </summary>
    public partial class TableViewController : IUITableViewDropDelegate
    {
        /// <summary>
        /// Ensure that the drop session contains a drag item with a data representation that the view can consume.
        /// </summary>
        [Export("tableView:canHandleDropSession:")]
        public bool CanHandleDropSession(UITableView tableView, IUIDropSession session)
        {
            return session.CanLoadObjects(typeof(NSString));
        }

        /// <summary>
        /// A drop proposal from a table view includes two items: a drop operation,
        /// typically .move or .copy; and an intent, which declares the action the
        /// table view will take upon receiving the items. (A drop proposal from a
        /// custom view does includes only a drop operation, not an intent.)
        /// </summary>
        [Export("tableView:dropSessionDidUpdate:withDestinationIndexPath:")]
        public UITableViewDropProposal DropSessionDidUpdate(UITableView tableView, IUIDropSession session, NSIndexPath destinationIndexPath)
        {
            // The .move operation is available only for dragging within a single app.
            if (tableView.HasActiveDrag)
            {
                if (session.Items.Length > 1)
                {
                    return new UITableViewDropProposal(UIDropOperation.Cancel);
                }
                else
                {
                    return new UITableViewDropProposal(UIDropOperation.Move, UITableViewDropIntent.InsertAtDestinationIndexPath);
                }
            }
            else
            {
                return new UITableViewDropProposal(UIDropOperation.Copy, UITableViewDropIntent.InsertAtDestinationIndexPath);
            }
        }

        /// <summary>
        /// his delegate method is the only opportunity for accessing and loading
        /// the data representations offered in the drag item.The drop coordinator
        /// supports accessing the dropped items, updating the table view, and specifying
        /// optional animations.Local drags with one item go through the existing
        /// `tableView(_:moveRowAt:to:)` method on the data source.
        /// </summary>
        public void PerformDrop(UITableView tableView, IUITableViewDropCoordinator coordinator)
        {
            NSIndexPath indexPath, destinationIndexPath;

            // TODO: confirm this port is accurate
            if (coordinator.DestinationIndexPath != null)
            {
                indexPath = coordinator.DestinationIndexPath;
                destinationIndexPath = indexPath;
            }
            else
            {
                // Get last index path of table view
                //var section = tableView.NumberOfSections() - 1;
                var row = tableView.NumberOfRowsInSection(0);
                destinationIndexPath = NSIndexPath.FromRowSection(row, 0); // HACK: hardcode section for debugging
            }

            coordinator.Session.LoadObjects<NSString>((items) =>
            {
                if (items.Length <= 0) return;

                // Consume drag items
                List<string> stringItems = new List<string>();
                foreach (var i in items)
                {
                    var q = NSString.FromHandle(i.Handle);
                    stringItems.Add(q.ToString());
                }
                var indexPaths = new List<NSIndexPath>();

                tableView.BeginUpdates();
                for (var j = 0; j < stringItems.Count; j++)
                {
                    var indexPath1 = NSIndexPath.FromRowSection(destinationIndexPath.Row + j, 0);

                    InsertTodo(new TodoItem { Name = stringItems[j] }, destinationIndexPath.Row); 
                    todoItems = AppDelegate.Current.TodoMgr.GetOrderedTodos().ToList(); 

                    indexPaths.Add(indexPath1);
                }
                if (indexPaths.Count > 0 && indexPaths.Count == stringItems.Count)
                {
                    tableView.InsertRows(indexPaths.ToArray(), UITableViewRowAnimation.Automatic);
                }
                tableView.EndUpdates();
                tableView.ReloadData();
            });
        }
    }
}
