// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Watch8Ball
{
	[Register ("Watch8BallViewController")]
	partial class Watch8BallViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton shakeButton { get; set; }

		[Action ("shakeButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void shakeButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (shakeButton != null) {
				shakeButton.Dispose ();
				shakeButton = null;
			}
		}
	}
}
