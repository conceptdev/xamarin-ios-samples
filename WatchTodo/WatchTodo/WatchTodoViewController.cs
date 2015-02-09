using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Linq;

using WormHoleSharp;

namespace WatchTodo
{
	public partial class WatchTodoViewController : UIViewController
	{
		
		public WatchTodoViewController (IntPtr handle) : base (handle)
		{
		}

		List<TodoItem> items;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "WatchTodo";
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			items = AppDelegate.Current.Database.GetItems ().ToList();

			todoItemTableView.Source = new TodoItemSource (items);

			AppDelegate.Current.wormHole.ListenForMessage<string> ("buttonMessage", (message) => {
				Console.WriteLine("Message Received: " + message);
				BeginInvokeOnMainThread (() => {
					// refresh
					todoItemTableView.Source = new TodoItemSource (items);
				});
			});
		}
		public override void ViewWillDisappear (bool animated)
		{
			AppDelegate.Current.wormHole.StopListeningForMessageWithIdentifier (WormholeMessage.MessageType);

			base.ViewWillDisappear (animated);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "addTodoItem") { // set in Storyboard
				var navctlr = segue.DestinationViewController as WatchTodoItemViewController;
				if (navctlr != null) {
					navctlr.SetTodoItem (new TodoItem());
				}
			} else if (segue.Identifier == "editTodoItem") { // set in Storyboard
				var navctlr = segue.DestinationViewController as WatchTodoItemViewController;
				if (navctlr != null) {
					var source = todoItemTableView.Source as TodoItemSource;
					var rowPath = todoItemTableView.IndexPathForSelectedRow;
					var item = source.GetItem (rowPath.Row);
					navctlr.SetTodoItem (item);
				}
			}
		}

//		public override void ViewDidAppear (bool animated)
//		{
//			base.ViewDidAppear (animated);
//		}
//
//		public override void ViewWillDisappear (bool animated)
//		{
//			base.ViewWillDisappear (animated);
//		}
//
//		public override void ViewDidDisappear (bool animated)
//		{
//			base.ViewDidDisappear (animated);
//		}
//
//		public override void DidReceiveMemoryWarning ()
//		{
//			// Releases the view if it doesn't have a superview.
//			base.DidReceiveMemoryWarning ();
//
//			// Release any cached data, images, etc that aren't in use.
//		}

	}
}

