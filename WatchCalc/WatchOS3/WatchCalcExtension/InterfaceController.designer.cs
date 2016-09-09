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

namespace WatchCalcExtension
{
	[Register ("InterfaceController")]
	partial class InterfaceController
	{
		[Outlet]
		WatchKit.WKInterfaceLabel display { get; set; }

		[Outlet]
		WatchKit.WKInterfaceLabel displayResult { get; set; }

		[Action ("button0")]
		partial void button0 ();

		[Action ("button1")]
		partial void button1 ();

		[Action ("button2")]
		partial void button2 ();

		[Action ("button3")]
		partial void button3 ();

		[Action ("button4")]
		partial void button4 ();

		[Action ("button5")]
		partial void button5 ();

		[Action ("button6")]
		partial void button6 ();

		[Action ("button7")]
		partial void button7 ();

		[Action ("button8")]
		partial void button8 ();

		[Action ("button9")]
		partial void button9 ();

		[Action ("buttonPlus")]
		partial void buttonPlus ();

		[Action ("buttonPoint")]
		partial void buttonPoint ();

		[Action ("clearMenu")]
		partial void clearMenu ();

		[Action ("closeMenu")]
		partial void closeMenu ();

		void ReleaseDesignerOutlets ()
		{
			if (display != null) {
				display.Dispose ();
				display = null;
			}
			if (displayResult != null) {
				displayResult.Dispose ();
				displayResult = null;
			}
		}
	}

}
