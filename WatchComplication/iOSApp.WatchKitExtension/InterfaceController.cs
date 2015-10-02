using System;

using WatchKit;
using Foundation;
using ClockKit;

namespace WatchComplication
{
	public partial class InterfaceController : WKInterfaceController
	{
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


		partial void UpdateClicked()
		{
			var complicationServer = CLKComplicationServer.SharedInstance; // is null :-(
			foreach (var complication in complicationServer.ActiveComplications) {
				complicationServer.ReloadTimeline(complication);
			}
		}
	}
}

