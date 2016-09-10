//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;


namespace Watch8BallApp.W8BallExtension
{
	[Register ("InterfaceController")]
	partial class InterfaceController
	{
		[Outlet]
		WatchKit.WKInterfaceLabel result { get; set; }

		[Action ("shake")]
		partial void shake ();

		void ReleaseDesignerOutlets ()
		{
			if (result != null) {
				result.Dispose ();
				result = null;
			}
		}
	}
}
