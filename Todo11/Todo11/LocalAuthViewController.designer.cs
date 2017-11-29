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
	[Register ("LocalAuthViewController")]
	partial class LocalAuthViewController
	{
		[Outlet]
		UIKit.UILabel HeadingLabel { get; set; }

		[Outlet]
		UIKit.UIButton LoginButton { get; set; }

		[Outlet]
		UIKit.UILabel LoginLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeadingLabel != null) {
				HeadingLabel.Dispose ();
				HeadingLabel = null;
			}

			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}

			if (LoginLabel != null) {
				LoginLabel.Dispose ();
				LoginLabel = null;
			}
		}
	}
}
