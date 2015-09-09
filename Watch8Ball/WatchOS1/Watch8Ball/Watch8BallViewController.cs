using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace Watch8Ball
{
	public partial class Watch8BallViewController : UIViewController
	{
		public Watch8BallViewController (IntPtr handle) : base (handle)
		{
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
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
		#endregion

		partial void shakeButton_TouchUpInside (UIButton sender)
		{
			// create the notification
			var notification = new UILocalNotification();

			// set the fire date (the date time in which it will fire)
			notification.FireDate = NSDate.Now.AddSeconds(10); //DateTime.Now.AddSeconds(10));
			notification.TimeZone = NSTimeZone.DefaultTimeZone;
			// configure the alert stuff
			notification.AlertTitle = "8Ball";
			notification.AlertAction = "View Alert";
			notification.AlertBody = "Your 10 sec alert has fired!";

			notification.UserInfo = NSDictionary.FromObjectAndKey (new NSString("8Ball Local Notification"), new NSString("Notification"));

			// modify the badge
			notification.ApplicationIconBadgeNumber = 1;

			// set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

			// schedule it
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
		}

	}
}

