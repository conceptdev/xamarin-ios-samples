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

namespace WatchTodoExtension
{
	[Register ("TodoRowController")]
	partial class TodoRowController
	{
		[Outlet]
		WatchKit.WKInterfaceImage DoneImage { get; set; }

		[Outlet]
		WatchKit.WKInterfaceSeparator Line { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel Name { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DoneImage != null) {
				DoneImage.Dispose ();
				DoneImage = null;
			}
			if (Line != null) {
				Line.Dispose ();
				Line = null;
			}
			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}
		}
	}
}
