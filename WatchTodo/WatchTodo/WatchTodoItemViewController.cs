using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WatchTodo
{
	partial class WatchTodoItemViewController : UIViewController
	{
		public WatchTodoItemViewController (IntPtr handle) : base (handle)
		{
		}
		TodoItem item;
		public void SetTodoItem (TodoItem tdi) {
			item = tdi;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Name.Text = item.Name;
			Notes.Text = item.Notes;
			Done.On = item.Done;

			if (item.ID <= 0)
				Delete.SetTitle ("Cancel", UIControlState.Normal);
			else
				Delete.SetTitle ("Delete", UIControlState.Normal);
		}

		partial void Save_TouchUpInside (UIButton sender)
		{
			item.Done = Done.On;
			item.Notes = Notes.Text;
			item.Name = Name.Text;

			AppDelegate.Current.Database.SaveItem(item);

			NavigationController.PopViewController(true);
		}

		partial void Delete_TouchUpInside (UIButton sender)
		{
			if (item.ID > 0)
				AppDelegate.Current.Database.DeleteItem (item.ID);

			NavigationController.PopViewController(true);
		}
	}
}
