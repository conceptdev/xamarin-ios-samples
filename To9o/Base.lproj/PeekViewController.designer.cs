// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StoryboardTables
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
			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}

			if (Notes != null) {
				Notes.Dispose ();
				Notes = null;
			}

			if (Done != null) {
				Done.Dispose ();
				Done = null;
			}
		}
	}
}
