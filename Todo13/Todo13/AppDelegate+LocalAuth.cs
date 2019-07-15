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
        #region LocalAuthentication
        public override void DidEnterBackground(UIApplication application)
        {
            (Window.RootViewController as NavigationController).Authenticated = false;
        }
        public override void WillEnterForeground(UIApplication application)
        {
            (Window.RootViewController as NavigationController).Authenticate();
        }
        #endregion
    }
}
