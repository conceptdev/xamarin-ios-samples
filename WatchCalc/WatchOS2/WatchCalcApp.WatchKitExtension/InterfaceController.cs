using System;

using WatchKit;
using Foundation;

namespace WatchCalcExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		string displayText = "", displayResultText = "";
		double arg1 = 0, arg2 = 0, result = 0;

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

		void filterButton (string num) {
			if (displayText == "0") {
				displayText = num;
			} else {
				displayText += num;
			}
			Console.WriteLine ("button: " + displayText);
			display.SetText(displayText);
		}

		partial void button0 () {
			filterButton ("0");
		}
		partial void button1 () {
			filterButton ("1");
		}
		partial void button2 () {
			filterButton ("2");
		}
		partial void button3 () {
			filterButton ("3");
		}
		partial void button4 () {
			filterButton ("4");
		}
		partial void button5 () {
			filterButton ("5");
		}
		partial void button6 () {
			filterButton ("6");
		}
		partial void button7 () {
			filterButton ("7");
		}
		partial void button8 () {
			filterButton ("8");
		}
		partial void button9 () {
			filterButton ("9");
		}
		partial void buttonPlus () {
			// set top to last number
			double t1;
			if (double.TryParse(displayText, out t1)) {

				arg2 = t1;

				result = arg1 + arg2;


				Console.WriteLine("result = " + result);

				arg1 = result;
				displayResultText = result.ToString();
				displayResult.SetText(displayResultText);

				displayText = "0";
//				display.SetText(displayText);
			}
			display.SetText("0");
		}
		partial void buttonPoint () {
			if (displayText.Contains(".")) {
				// noop
			} else {
				displayText += ".";
			}
			display.SetText(displayText);
		}
		partial void clearMenu ()
		{
			displayResultText = "-";
			displayResult.SetText(displayResultText);

			displayText = "0";
			display.SetText(displayText);

			result = arg1 = arg2 = 0; 
		}
	}
}