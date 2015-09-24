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
	[Register ("AllViewCell")]
	partial class AllViewCell
	{
		[Outlet]
		UIKit.UIImageView DoneImage { get; set; }

		[Outlet]
		UIKit.UILabel TodoName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TodoName != null) {
				TodoName.Dispose ();
				TodoName = null;
			}

			if (DoneImage != null) {
				DoneImage.Dispose ();
				DoneImage = null;
			}
		}
	}
}
