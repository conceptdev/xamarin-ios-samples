using System;

using Foundation;
using WatchKit;

namespace WatchComplication
{
	[Register ("ExtensionDelegate")]
	public class ExtensionDelegate : WKExtensionDelegate
	{
		public ExtensionDelegate ()
		{
		}

		public override void ApplicationDidBecomeActive ()
		{
			Console.WriteLine ("ApplicationDidBecomeActive");
		}
		public override void ApplicationDidFinishLaunching ()
		{
			Console.WriteLine ("ApplicationDidFinishLaunching");
		}
		public override void ApplicationWillResignActive ()
		{
			Console.WriteLine ("ApplicationWillResignActive");
		}
		public override void DidReceiveLocalNotification (UIKit.UILocalNotification notification)
		{
			Console.WriteLine ("DidReceiveLocalNotification");
		}
		public override void DidReceiveRemoteNotification (NSDictionary userInfo)
		{
			Console.WriteLine ("DidReceiveRemoteNotification");
		}
		public override void HandleUserActivity (NSDictionary userInfo)
		{
			Console.WriteLine ("HandleUserActivity");
		}
		public override void HandleAction (string identifier, NSDictionary remoteNotification)
		{
			throw new System.NotImplementedException ();
		}
		public override void HandleAction (string identifier, NSDictionary remoteNotification, NSDictionary responseInfo)
		{
			throw new System.NotImplementedException ();
		}
		public override void HandleAction (string identifier, UIKit.UILocalNotification localNotification)
		{
			throw new System.NotImplementedException ();
		}
		public override void HandleAction (string identifier, UIKit.UILocalNotification localNotification, NSDictionary responseInfo)
		{
			throw new System.NotImplementedException ();
		}

	}
}


