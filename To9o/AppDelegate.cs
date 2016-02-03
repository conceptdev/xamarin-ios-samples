using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using SQLite;
using CoreSpotlight;
using Xamarin;

namespace StoryboardTables
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}

		#region Database set-up
		public static AppDelegate Current { get; private set; }
		public TaskManager TaskMgr { get; set; }
		SQLite.SQLiteConnection conn;
		#endregion


		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Current = this;

			#region Database set-up
			var sqliteFilename = "TaskDB.db3";
			// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
			// (they don't want non-user-generated data in Documents)
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new SQLite.SQLiteConnection(path);
			TaskMgr = new TaskManager(conn);
			#endregion

			Console.WriteLine ("bbbbbbbbbb FinishedLaunching");

			#region Quick Action
			var shouldPerformAdditionalDelegateHandling = true;

			// Get possible shortcut item
			if (launchOptions != null) {
				LaunchedShortcutItem = launchOptions [UIApplication.LaunchOptionsShortcutItemKey] as UIApplicationShortcutItem;
				shouldPerformAdditionalDelegateHandling = (LaunchedShortcutItem == null);
			}
			#endregion


			#region Xamarin.Insights
			// HACK: hardcoded identify
			// iPhone 6s
			var traits = new Dictionary<string, string> {
				{Insights.Traits.Email, "john.doe@xamarin.com"},
				{Insights.Traits.Name, "John Doe"}
			};
			Insights.Identify("0", traits);
			#endregion


			return shouldPerformAdditionalDelegateHandling;
		}

		#region Quick Action
		//
		// if app is already running (otherwise went through FinishedLaunching)
		public override void PerformActionForShortcutItem (UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
		{
			Console.WriteLine ("dddddddd PerformActionForShortcutItem");
			// Perform action
			var handled = HandleShortcutItem(shortcutItem);
			completionHandler(handled);
		}

		public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }
		public override void OnActivated (UIApplication application)
		{
			Console.WriteLine ("ccccccc OnActivated");

			// Handle any shortcut item being selected
			HandleShortcutItem(LaunchedShortcutItem);

			// Clear shortcut after it's been handled
			LaunchedShortcutItem = null;
		}


		public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem) {
			Console.WriteLine ("eeeeeeeeeee HandleShortcutItem ");
			var handled = false;

			// Anything to process?
			if (shortcutItem == null) return false;

			// Take action based on the shortcut type
			switch (shortcutItem.Type) {
			case ShortcutIdentifiers.Add:
				Console.WriteLine ("QUICKACTION: Add new item");

				Insights.Track("3DTouch", new Dictionary<string, string> {
					{"Type", "NewItem"}
				});

				handled = true;
				break;
			case ShortcutIdentifiers.Share:
				Console.WriteLine ("QUICKACTION: Share summary of items");
				handled = true;
				break;
			}

			//HACK: show the detail viewcontroller
			ContinueNavigation ();

			Console.Write (handled);
			// Return results
			return handled;
		}
		#endregion

		#region NSUserActivity AND CoreSpotlight
		// http://www.raywenderlich.com/84174/ios-8-handoff-tutorial
		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			Console.WriteLine ("ffffffff ContinueUserActivity");

			UIViewController tvc = null;

			// NSUserActivity
			if ((userActivity.ActivityType == ActivityTypes.Add)
				|| (userActivity.ActivityType == ActivityTypes.Detail))
			{
				if (userActivity.UserInfo.Count == 0) {
					// new item

				} else {
					var uid = userActivity.UserInfo.ObjectForKey ((NSString)"id").ToString ();
					if (uid == "0") {
						Console.WriteLine ("No userinfo found for " + ActivityTypes.Detail);
					} else {
						Console.WriteLine ("Should display id " + uid);
						// handled in DetailViewController.RestoreUserActivityState
					}
				}
				tvc = ContinueNavigation ();

				Insights.Track("SearchResult", new Dictionary<string, string> {
					{"Type", "NSUserActivity"}
				});
			}
			// CoreSpotlight
			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				var uid = userActivity.UserInfo.ObjectForKey 
							(CSSearchableItem.ActivityIdentifier).ToString();

				System.Console.WriteLine ("Show the detail for id:" + uid);

				tvc = ContinueNavigation ();

				Insights.Track("SearchResult", new Dictionary<string, string> {
					{"Type", "CoreSpotlight"}
				});
			}
			completionHandler(new NSObject[] {tvc});

			return true;
		}
		#endregion

		//
		// called for Quick Action AND NSUserActivity
		UIViewController ContinueNavigation (){
			Console.WriteLine ("gggggggggg ContinueNavigation");

			// 1. load screen
			var sb = UIStoryboard.FromName ("MainStoryboard", null);
			var tvc = sb.InstantiateViewController("detailvc") as DetailViewController;

			// 2. set initial state
			var r = Window.RootViewController as UINavigationController;
			r.PopToRootViewController (false);

			// 3. populate and display screen
			tvc.SetTodo (new Task {Name="(new action)"}); // from 3D Touch menu
			tvc.Delegate = CollectionController.Current;
			r.PushViewController (tvc, false);
			return tvc;
		}
	}
}
