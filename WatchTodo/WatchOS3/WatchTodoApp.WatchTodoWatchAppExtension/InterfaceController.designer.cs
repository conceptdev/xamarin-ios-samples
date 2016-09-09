//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace WatchTodoExtension
{
	[Register ("InterfaceController")]
	partial class InterfaceController
	{
		[Outlet]
		WatchKit.WKInterfaceTable TodoTable { get; set; }

		[Action ("New")]
		partial void New ();

		void ReleaseDesignerOutlets ()
		{
			if (TodoTable != null) {
				TodoTable.Dispose ();
				TodoTable = null;
			}
		}
	}
}

