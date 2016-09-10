using System;

using WatchKit;
using Foundation;

namespace iOSApp.WatchAppExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		protected InterfaceController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void Awake(NSObject context)
		{
			base.Awake(context);

			// Configure interface objects here.
			Console.WriteLine("{0} awake with context", this);
		}

		public override void WillActivate()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine("{0} will activate", this);
		}

		public override void DidDeactivate()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine("{0} did deactivate", this);
		}

		//partial void UpdateClicked()
		//{
		//	Console.WriteLine("BUTTON CLICKED");
		//	var complicationServer = CLKComplicationServer.SharedInstance; // is null :-(
		//	if (complicationServer.ActiveComplications != null)
		//	{
		//		Console.WriteLine("Active complications!!!!!!!!!!");
		//		foreach (var complication in complicationServer.ActiveComplications)
		//		{
		//			Console.WriteLine("Active " + complication.Description ?? "null");

		//			complicationServer.ReloadTimeline(complication);
		//		}
		//	}
		//	else {
		//		Console.WriteLine("No active complications");
		//	}
		//}
	}
}
