using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using SQLite;
using CoreSpotlight;

namespace StoryboardTables
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		
		public override UIWindow Window {
			get;
			set;
		}

		public static AppDelegate Current { get; private set; }
		public TaskManager TaskMgr { get; set; }
		SQLite.SQLiteConnection conn;

		public override void FinishedLaunching (UIApplication application)
		{
			Current = this;

			var sqliteFilename = "TaskDB.db3";
			// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
			// (they don't want non-user-generated data in Documents)
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..","Library/"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new SQLite.SQLiteConnection(path);
			TaskMgr = new TaskManager(conn);

			SearchModel = new iOS9SearchModel (TaskMgr.GetTasks().ToList());

			Console.WriteLine ("aaaaaaaaa FinishedLaunching");
		}

		public iOS9SearchModel SearchModel {
			get;
			private set;
		}

		// http://www.raywenderlich.com/84174/ios-8-handoff-tutorial
		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				var uid = userActivity.UserInfo.ObjectForKey (CSSearchableItem.ActivityIdentifier).ToString();

				System.Console.WriteLine ("Show the page for " + uid);

				var restaurantName = SearchModel.Lookup (uid);

				System.Console.WriteLine ("which is " + restaurantName);

				var sb = UIStoryboard.FromName ("MainStoryboard", null);
				var tvc = sb.InstantiateViewController("detail") as TaskDetailViewController;
//				var source = tvc.TableView.Source as RootTableSource;

				var item = TaskMgr.GetTasks ().ToList () [0];
				tvc.SetTask(Window.RootViewController as RootViewController, item);
				Console.WriteLine ("xxxxxxxxxx ContinueUserActivity");


				//HACK: need to open detailviewcontroller here
				completionHandler(new NSObject[] {tvc});

				var r = Window.RootViewController as UINavigationController;
				r.PopToRootViewController (false);
				r.PushViewController (tvc, false);

//				var r = Window.RootViewController.ChildViewControllers [0];
//				r.NavigationController.PopToRootViewController (false);
//				r.NavigationController.PushViewController (tvc, false);
//				var rvc = application.KeyWindow.RootViewController as RootViewController;
//				rvc.NavigationController.PushViewController(tvc, false);
			}
			return true;
		}

		//
		// This method is invoked when the application is about to move from active to inactive state.
		//
		// OpenGL applications should use this method to pause.
		//
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed. 
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

