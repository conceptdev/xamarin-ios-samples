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

namespace To11oApp
{
	[Register ("TodoCell")]
	partial class TodoCell
	{
		[Outlet]
		UIKit.UIImageView DoneImage { get; set; }

		[Outlet]
		UIKit.UILabel TodoName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DoneImage != null) {
				DoneImage.Dispose ();
				DoneImage = null;
			}
			if (TodoName != null) {
				TodoName.Dispose ();
				TodoName = null;
			}
		}
	}
}
