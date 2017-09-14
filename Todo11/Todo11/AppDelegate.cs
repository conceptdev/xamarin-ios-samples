using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using SQLite;
using CoreSpotlight;

using Intents;
using UserNotifications;

namespace Todo11App
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

		public static INSiriAuthorizationStatus SiriAuthorizationStatus { get; set; }

		#region Database set-up
		public static AppDelegate Current { get; private set; }
		public TodoManager TodoMgr { get; set; }
		SQLite.SQLiteConnection conn;
		#endregion


		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Current = this;

			

			#region Database set-up
			var sqliteFilename = "TodoDB.db3";
			// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
			// (they don't want non-user-generated data in Documents)
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new SQLite.SQLiteConnection(path);
			TodoMgr = new TodoManager(conn);
			#endregion

			#region Theme
			UINavigationBar.Appearance.TintColor = UIColor.FromRGB (0x5A, 0x86, 0x22); // 5A8622 dark-green
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB (0xCF, 0xEF, 0xa7); // CFEFA7 light-green

			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() {
				TextColor = UIColor.FromRGB (0x5A, 0x86, 0x22), // 5A8622 dark-green
			    TextShadowColor = UIColor.Clear
			}); 
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

			}
			// CoreSpotlight
			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				var uid = userActivity.UserInfo.ObjectForKey 
							(CSSearchableItem.ActivityIdentifier).ToString();

				System.Console.WriteLine ("Show the detail for id:" + uid);

				tvc = ContinueNavigation ();

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
			tvc.SetTodo (new TodoItem {Name="(new action)"}); // from 3D Touch menu
			tvc.Delegate = CollectionController.Current;
			r.PushViewController (tvc, false);
			return tvc;
		}


	}
}
