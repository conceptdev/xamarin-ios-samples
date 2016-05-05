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
	[Register ("CollectionController")]
	partial class CollectionController
	{
		[Outlet]
		UIKit.UIBarButtonItem AboutButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem AddButton { get; set; }

		[Outlet]
		UIKit.UICollectionView Collection { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AboutButton != null) {
				AboutButton.Dispose ();
				AboutButton = null;
			}
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
			if (Collection != null) {
				Collection.Dispose ();
				Collection = null;
			}
		}
	}
}
