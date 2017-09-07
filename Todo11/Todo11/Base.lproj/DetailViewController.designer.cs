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
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UISwitch DoneSwitch { get; set; }

		[Outlet]
		UIKit.UILabel ForText { get; set; }

		[Outlet]
		UIKit.UITextField NameText { get; set; }

		[Outlet]
		UIKit.UITextField NotesText { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
			if (DoneSwitch != null) {
				DoneSwitch.Dispose ();
				DoneSwitch = null;
			}
			if (ForText != null) {
				ForText.Dispose ();
				ForText = null;
			}
			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}
			if (NotesText != null) {
				NotesText.Dispose ();
				NotesText = null;
			}
			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
		}
	}
}
