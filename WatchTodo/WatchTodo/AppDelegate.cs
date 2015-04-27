using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using System.IO;
using SQLite;
using WormHoleSharp;

namespace WatchTodo
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
		public TodoItemDatabase Database { get; set; }
		public Wormhole wormHole {get;set;}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Current = this;

			wormHole = new Wormhole ("group.co.conceptdev.WatchTodo", "messageDir");

			var FileManager = new NSFileManager ();
			var appGroupContainer = FileManager.GetContainerUrl ("group.co.conceptdev.WatchTodo");
			var appGroupContainerPath = appGroupContainer.Path;
			Console.WriteLine ("agcpath: " + appGroupContainerPath);


			var sqliteFilename = "TodoSQLite.db3";
			// App Group storage, shared with Watch Extension
			var path = Path.Combine(appGroupContainerPath, sqliteFilename);
			var conn = new SQLiteConnection (path);

			Database = new TodoItemDatabase(conn);


			// HACK: temporary population of data
			if (Database.GetItems ().Count() == 0) {
				Database.SaveItem (new TodoItem { Name = "buy pineapple" });
				Database.SaveItem (new TodoItem { Name = "buy dragon fruit", Done = true });
				Database.SaveItem (new TodoItem { Name = "buy honeydew" });
				Database.SaveItem (new TodoItem { Name = "buy rockmelon" });
			}


			return true;
		}

		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
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

