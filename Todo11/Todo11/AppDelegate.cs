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

			UINavigationBar.Appearance.LargeTitleTextAttributes = new UIStringAttributes
			{
				ForegroundColor = UIColor.FromRGB(0x5A, 0x86, 0x22), // 5A8622 dark-green
			};

            //UIButton.AppearanceWhenContainedIn(typeof(UITableView)).SetTitleColor(UIColor.FromRGB(0x5A, 0x86, 0x22), UIControlState.Normal);
            UIButton.AppearanceWhenContainedIn(typeof(UITableView)).TintColor = UIColor.FromRGB(0x5A, 0x86, 0x22);
			#endregion

			Console.WriteLine ("bbbbbbbbbb FinishedLaunching");

			#region Quick Action
			var shouldPerformAdditionalDelegateHandling = true;

			// Get possible shortcut item
			if (launchOptions != null)
            {
				LaunchedShortcutItem = launchOptions [UIApplication.LaunchOptionsShortcutItemKey] as UIApplicationShortcutItem;
				shouldPerformAdditionalDelegateHandling = (LaunchedShortcutItem == null);
			}
			#endregion

			return shouldPerformAdditionalDelegateHandling;
		}
	}
}
