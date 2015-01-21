using System;

using WatchKit;
using Foundation;

namespace WatchCalcExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		string displayText = "";


		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}

		partial void button0 () {
			displayText += "0";
			display.SetText(displayText);
		}
		partial void button1 () {
			displayText += "1";
			display.SetText(displayText);
		}
		partial void button2 () {
			displayText += "2";
			display.SetText(displayText);
		}
		partial void button3 () {
			displayText += "3";
			display.SetText(displayText);
		}
		partial void button4 () {
			displayText += "4";
			display.SetText(displayText);
		}
		partial void button5 () {
			displayText += "5";
			display.SetText(displayText);
		}
		partial void button6 () {
			displayText += "6";
			display.SetText(displayText);
		}
		partial void button7 () {
			displayText += "7";
			display.SetText(displayText);
		}
		partial void button8 () {
			displayText += "8";
			display.SetText(displayText);
		}
		partial void button9 () {
			displayText += "9";
			display.SetText(displayText);
		}
	}
}

