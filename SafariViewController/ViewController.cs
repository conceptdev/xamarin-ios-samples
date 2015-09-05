using System;

using UIKit;
using SafariServices;
using Foundation;

namespace SafariViewDemo
{
	public partial class ViewController : UIViewController, ISFSafariViewControllerDelegate
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			openButton.TouchUpInside += (sender, e) => {
				var sfvc = new SFSafariViewController (new NSUrl("http://xamarin.com"),true);

				PresentViewController(sfvc, true, null);
			};
				
		}

		[Foundation.Export ("safariViewControllerDidFinish:")]
		public void DidFinish (SFSafariViewController controller)
		{
			DismissViewController (true, null);
		}
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

