using System;

using Foundation;
using WatchKit;
using ClockKit;

namespace WatchComplication
{
	[Register ("ExtensionDelegate")]
	public class ExtensionDelegate : WKExtensionDelegate
	{
		public ExtensionDelegate (IntPtr p) : base (p) 
		{
			Console.WriteLine ("ExtensionDelegate ctor IntPtr");
		}
		public override void ApplicationDidBecomeActive ()
		{
			Console.WriteLine ("{0} ApplicationDidBecomeActive", this);
		}
		public override void ApplicationDidFinishLaunching ()
		{
			Console.WriteLine ("{0} ApplicationDidFinishLaunching", this);

			var complicationServer = CLKComplicationServer.SharedInstance; // is null :-(
			if (complicationServer.ActiveComplications != null) {
				Console.WriteLine ("Active complications!");
				foreach (var complication in complicationServer.ActiveComplications) {
					complicationServer.ReloadTimeline (complication);
				}
			} else {
				Console.WriteLine ("No active complications");
			}
		}
		public override void ApplicationWillResignActive ()
		{
			Console.WriteLine ("{0} ApplicationWillResignActive", this);
		}
	}
}