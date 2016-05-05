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

namespace To9oApp
{
	[Register ("PeekViewController")]
	partial class PeekViewController
	{
		[Outlet]
		UIKit.UIImageView Done { get; set; }

		[Outlet]
		UIKit.UILabel Name { get; set; }

		[Outlet]
		UIKit.UILabel Notes { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Done != null) {
				Done.Dispose ();
				Done = null;
			}
			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}
			if (Notes != null) {
				Notes.Dispose ();
				Notes = null;
			}
		}
	}
}
