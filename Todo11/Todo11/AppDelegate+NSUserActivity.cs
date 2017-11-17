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
    public partial class AppDelegate
    {
        //
        // if app is already running (otherwise went through FinishedLaunching)
        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            Console.WriteLine("dddddddd PerformActionForShortcutItem");
            // Perform action
            var handled = HandleShortcutItem(shortcutItem);
            completionHandler(handled);
        }

        public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }
        public override void OnActivated(UIApplication application)
        {
            Console.WriteLine("ccccccc OnActivated");

            // Handle any shortcut item being selected
            HandleShortcutItem(LaunchedShortcutItem);

            // Clear shortcut after it's been handled
            LaunchedShortcutItem = null;
        }


        public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem)
        {
            Console.WriteLine("eeeeeeeeeee HandleShortcutItem ");
            var handled = false;

            // Anything to process?
            if (shortcutItem == null) return false;

            // Take action based on the shortcut type
            switch (shortcutItem.Type)
            {
                case ShortcutIdentifiers.Add:
                    Console.WriteLine("QUICKACTION: Add new item");

                    handled = true;
                    break;
                case ShortcutIdentifiers.Share:
                    Console.WriteLine("QUICKACTION: Share summary of items");
                    handled = true;
                    break;
            }

            //HACK: show the detail viewcontroller
            ContinueNavigation();

            Console.Write(handled);
            // Return results
            return handled;
        }

        #region NSUserActivity AND CoreSpotlight
        // http://www.raywenderlich.com/84174/ios-8-handoff-tutorial
        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            Console.WriteLine("ffffffff ContinueUserActivity");

            UIViewController tvc = null;

            // NSUserActivity
            if ((userActivity.ActivityType == ActivityTypes.Add)
                || (userActivity.ActivityType == ActivityTypes.Detail))
            {
                if (userActivity.UserInfo.Count == 0)
                {
                    // new item

                }
                else
                {
                    var uid = userActivity.UserInfo.ObjectForKey((NSString)"id").ToString();
                    if (uid == "0")
                    {
                        Console.WriteLine("No userinfo found for " + ActivityTypes.Detail);
                    }
                    else
                    {
                        Console.WriteLine("Should display id " + uid);
                        // handled in DetailViewController.RestoreUserActivityState
                    }
                }
                tvc = ContinueNavigation();

            }
            // CoreSpotlight
            if (userActivity.ActivityType == CSSearchableItem.ActionType)
            {
                var uid = userActivity.UserInfo.ObjectForKey
                            (CSSearchableItem.ActivityIdentifier).ToString();

                System.Console.WriteLine("Show the detail for id:" + uid);

                tvc = ContinueNavigation();

            }
            completionHandler(new NSObject[] { tvc });

            return true;
        }
        #endregion

        //
        // called for Quick Action AND NSUserActivity
        UIViewController ContinueNavigation()
        {
            Console.WriteLine("gggggggggg ContinueNavigation");

            // 1. load screen
            var sb = UIStoryboard.FromName("MainStoryboard", null);
            var tvc = sb.InstantiateViewController("detailvc") as DetailViewController;

            // 2. set initial state
            var r = Window.RootViewController as UINavigationController;
            r.PopToRootViewController(false);

            // 3. populate and display screen
            tvc.SetTodo(new TodoItem { Name = "(new action)" }); // from 3D Touch menu
            tvc.Delegate = TableViewController.Current;
            r.PushViewController(tvc, false);
            return tvc;
        }
    }
}
