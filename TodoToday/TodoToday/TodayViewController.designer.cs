// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace TodoToday
{
	[Register ("TodayViewController")]
	partial class TodayViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton PlayButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel todoLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PlayButton != null) {
				PlayButton.Dispose ();
				PlayButton = null;
			}
			if (todoLabel != null) {
				todoLabel.Dispose ();
				todoLabel = null;
			}
		}
	}
}
