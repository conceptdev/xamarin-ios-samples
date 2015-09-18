using System;
using CoreGraphics;
using System.Collections.Generic;
using Foundation;
using UIKit;
using System.Linq;

namespace StoryboardTables
{
	public partial class RootViewController : UITableViewController
	{
		// The list of tasks is NOT persisted, even though you can add and delete tasks
		// in this sample, the changes are only in memory and will disappear when the app restarts
		List<Task> tasks;

		public RootViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Todo", "");

			if (NSLocale.PreferredLanguages.Length > 0) {
				var pref = NSLocale.PreferredLanguages [0];
				Console.WriteLine ("preferred-language:" + pref + " of " + NSLocale.PreferredLanguages.Count());
			}

		}

		/// <summary>
		/// Prepares for segue.
		/// </summary>
		/// <remarks>
		/// The prepareForSegue method is invoked whenever a segue is about to take place. 
		/// The new view controller has been loaded from the storyboard at this point but 
        /// it’s not visible yet, and we can use this opportunity to send data to it.
		/// </remarks>
//		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
//		{
//			if (segue.Identifier == "detailsegue") { // set in Storyboard
//				var tvc = segue.DestinationViewController as DetailViewController;
//				if (tvc != null) {
//					var source = TableView.Source as RootTableSource;
//					var rowPath = TableView.IndexPathForSelectedRow;
//					var item = source.GetItem(rowPath.Row);
//					tvc.Delegate = this;
//					tvc.SetTodo(item);
//				}
//			}
//		}
//
//		public void CreateTask ()
//		{
//			// StackView
//			var detail = Storyboard.InstantiateViewController("detailvc") as DetailViewController;
//			detail.Delegate = this;
//			detail.SetTodo (new Task());
//			NavigationController.PushViewController (detail, true);
//
//			// Could to this instead of the above, but need to create 'new Task()' in PrepareForSegue()
//			//this.PerformSegue ("TaskSegue", this);
//		}
//		public void SaveTask (Task task) {
//			Console.WriteLine("Save "+task.Name);
//
//			AppDelegate.Current.TaskMgr.SaveTask(task);
//
//			iOS9SearchModel.Index (task);
//
//		}
//		public void DeleteTask (Task task) {
//			Console.WriteLine("Delete "+task.Name);
//			if (task.Id >= 0) {
//				AppDelegate.Current.TaskMgr.DeleteTask (task.Id);
//				iOS9SearchModel.Delete (task);
//			}
//		}






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
//				CreateTask ();
			};
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
//			ReleaseDesignerOutlets ();
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			tasks = AppDelegate.Current.TaskMgr.GetTasks ().ToList ();

			// bind every time, to reflect deletion in the Detail view
			TableView.Source = new RootTableSource(tasks.ToArray ());
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
		
		#endregion
	}
}

