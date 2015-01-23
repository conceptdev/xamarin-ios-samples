// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WatchTodoExtension
{
	[Register ("TodoDetailController")]
	partial class TodoDetailController
	{
		[Outlet]
		WatchKit.WKInterfaceSwitch Done { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel Name { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel Notes { get; set; }

		[Action ("DoneSwitched:")]
		partial void DoneSwitched (Foundation.NSObject value);

		[Action ("Save")]
		partial void Save ();
		
		void ReleaseDesignerOutlets ()
		{
			if (Done != null) {
				Done.Dispose ();
				Done = null;
			}

			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}

			if (Notes != null) {
				Notes.Dispose ();
				Notes = null;
			}
		}
	}
}
