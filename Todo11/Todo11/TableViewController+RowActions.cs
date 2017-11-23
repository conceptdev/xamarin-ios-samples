using Foundation;
using System;
using UIKit;

namespace Todo11App
{
    public partial class TableViewController 
    {
        // Swipe left
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }
        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {
            return NSBundle.MainBundle.LocalizedString("Delete", "");
        }
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    DeleteTodo(todoItems[indexPath.Row]);
                    todoItems.RemoveAt(indexPath.Row);
                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }

        // Swipe right
        public UIContextualAction ContextualDoneAction(int row)
        {
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                                                                      NSBundle.MainBundle.LocalizedString("Done", ""),
                                                                      (FlagAction, view, success) => {
                                                                          ToggleTodoDone(todoItems[row]);
                                                                          TableView.ReloadData(); // otherwise ticks don't get updated
                                                                          success(true);
                                                                      });
            action.Image = todoItems[row].Done ? UIImage.FromBundle("box") : UIImage.FromBundle("checkbox");
            action.BackgroundColor = UIColor.FromRGB(0x5A, 0x86, 0x22); // dark green

            return action;
        }
        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var doneAction = ContextualDoneAction(indexPath.Row);
            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { doneAction });
            leadingSwipe.PerformsFirstActionWithFullSwipe = true;
            return leadingSwipe;
        }
    }
}