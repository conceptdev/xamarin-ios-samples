// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

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
