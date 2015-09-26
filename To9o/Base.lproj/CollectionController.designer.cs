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
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (Collection != null) {
				Collection.Dispose ();
				Collection = null;
			}

			if (AboutButton != null) {
				AboutButton.Dispose ();
				AboutButton = null;
			}
		}
	}
}
