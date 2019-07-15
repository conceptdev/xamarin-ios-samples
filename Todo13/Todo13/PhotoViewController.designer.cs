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
	[Register ("PhotoViewController")]
	partial class PhotoViewController
	{
		[Outlet]
		UIKit.UIButton CameraButton { get; set; }

		[Outlet]
		UIKit.UILabel ClassificationLabel { get; set; }

		[Outlet]
		UIKit.UIButton CloseButton { get; set; }

		[Outlet]
		UIKit.UIButton GalleryButton { get; set; }

		[Outlet]
		UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CameraButton != null) {
				CameraButton.Dispose ();
				CameraButton = null;
			}

			if (GalleryButton != null) {
				GalleryButton.Dispose ();
				GalleryButton = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}

			if (CloseButton != null) {
				CloseButton.Dispose ();
				CloseButton = null;
			}

			if (ClassificationLabel != null) {
				ClassificationLabel.Dispose ();
				ClassificationLabel = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}
		}
	}
}
