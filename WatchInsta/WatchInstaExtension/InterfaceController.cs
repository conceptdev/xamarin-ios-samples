using System;

using WatchKit;
using Foundation;
using System.IO;
using UIKit;

namespace WatchInstaExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		string lastImageFilename = "";
		string lastImageName = "";

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			var FileManager = new NSFileManager ();
			var appGroupContainer = FileManager.GetContainerUrl ("group.com.conceptdevelopment.watchinsta");
			var appGroupContainerPath = appGroupContainer.Path;
			Console.WriteLine ("agcpath: " + appGroupContainerPath);

			var d = new DirectoryInfo (appGroupContainerPath);
			foreach (var f in d.EnumerateFiles()) {
				Console.WriteLine (f);
				lastImageFilename = f.FullName; 
				lastImageName = f.Name;
			}
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);

			using (var image = UIImage.FromFile (lastImageFilename)) {
				image1.SetImage (image);
			}

			Console.WriteLine ("'" +lastImageFilename+"' image should be loaded by now, attmept cache");


			var device = WKInterfaceDevice.CurrentDevice;
			using (var image = UIImage.FromFile (lastImageFilename)) {
				if (!device.AddCachedImage (image, lastImageName)) {
					Console.WriteLine ("Image cache full.");
				} else {
					image1.SetImage (lastImageName);
				}
			}
		
			Console.WriteLine ("cache: " + WKInterfaceDevice.CurrentDevice.WeakCachedImages);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}

