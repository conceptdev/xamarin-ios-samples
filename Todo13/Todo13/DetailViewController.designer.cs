// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Todo11App
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
		UIKit.UIImageView Photo { get; set; }

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

			if (Photo != null) {
				Photo.Dispose ();
				Photo = null;
			}
		}
	}
}
