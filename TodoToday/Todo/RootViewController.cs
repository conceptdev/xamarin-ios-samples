using System;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Todo;

namespace StoryboardTables
{
	public partial class RootViewController : UITableViewController
	{
		// The list of tasks is NOT persisted, even though you can add and delete tasks
		// in this sample, the changes are only in memory and will disappear when the app restarts
		List<TodoItem> tasks;

		public RootViewController (IntPtr handle) : base (handle)
		{
			Title = "TaskBoard";
		}

		/// <summary>
		/// Prepares for segue.
		/// </summary>
		/// <remarks>
		/// The prepareForSegue method is invoked whenever a segue is about to take place. 
		/// The new view controller has been loaded from the storyboard at this point but 
        /// it’s not visible yet, and we can use this opportunity to send data to it.
		/// </remarks>
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "TaskSegue") { // set in Storyboard
				var navctlr = segue.DestinationViewController as TaskDetailViewController;
				if (navctlr != null) {
					var source = TableView.Source as RootTableSource;
					var rowPath = TableView.IndexPathForSelectedRow;
					var item = source.GetItem(rowPath.Row);
					navctlr.SetTask(this, item);
				}
			}
		}

		public void CreateTask () {
			// first, add the task to the underlying data
			var newId = tasks[tasks.Count - 1].Id + 1;
			var newTask = new TodoItem(){Id=newId};
			tasks.Add (newTask);
			// then open the detail view to edit it
			var detail = Storyboard.InstantiateViewController("detail") as TaskDetailViewController;
			detail.SetTask (this, newTask);
			NavigationController.PushViewController (detail, true);

			// Could to this instead of the above, but need to create 'new Task()' in PrepareForSegue()
			//this.PerformSegue ("TaskSegue", this);
		}
		public void SaveTask (TodoItem task) {
			Console.WriteLine("Save "+task.Name);
			var oldTask = tasks.Find(t => t.Id == task.Id);
			oldTask = task;
			NavigationController.PopViewController(true);
		}
		public void DeleteTask (TodoItem task) {
			Console.WriteLine("Delete "+task.Name);
			var oldTask = tasks.Find(t => t.Id == task.Id);
			tasks.Remove (oldTask);
			NavigationController.PopViewController(true);
		}






		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		#region View lifecycle
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			AddButton.Clicked += (sender, e) => {
				CreateTask ();
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			tasks = AppDelegate.Database.GetItems ();

			// bind every time, to reflect deletion in the Detail view
			TableView.Source = new RootTableSource(tasks.ToArray ());

			NSUserDefaults shared = new NSUserDefaults("group.co.conceptdev.TodoToday", NSUserDefaultsType.SuiteName);
			shared.SetInt (tasks.Count, "TodoCount");
			shared.Synchronize ();
			Console.WriteLine ("Set NSUserDefaults TodoCount: " + tasks.Count);
		}
		
		#endregion
	}
}

