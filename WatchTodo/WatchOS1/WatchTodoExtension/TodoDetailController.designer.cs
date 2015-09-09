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
	[Register ("TodoDetailController")]
	partial class TodoDetailController
	{
		[Outlet]
		WatchKit.WKInterfaceSwitch Done { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel Name { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel Notes { get; set; }

		[Action ("DoneSwitch:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void DoneSwitch (WatchKit.WKInterfaceSwitch sender);

		[Action ("Save")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Save ();

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
