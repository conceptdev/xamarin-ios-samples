using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Intents;

namespace Todo11App
{
	public partial class NavigationController : UINavigationController
	{
		public NavigationController (IntPtr handle) : base (handle)
		{
		}

        public bool Authenticated { get; set; }= false;

        public override void ViewDidLoad()
        {
			// https://medium.com/@PavelGnatyuk/large-title-and-search-in-ios-11-514d5e020cee
			NavigationBar.PrefersLargeTitles = true;


			// Request access to Siri
			INPreferences.RequestSiriAuthorization((INSiriAuthorizationStatus status) => {
				// Respond to returned status
				switch (status)
				{
					case INSiriAuthorizationStatus.Authorized:
						Console.WriteLine("SiriKit Authorized");
						break;
					case INSiriAuthorizationStatus.Denied:
						Console.WriteLine("SiriKit Denied");
						break;
					case INSiriAuthorizationStatus.NotDetermined:
						Console.WriteLine("SiriKit Not Determined");
						break;
					case INSiriAuthorizationStatus.Restricted:
						Console.WriteLine("SiriKit Restricted");
						break;
				}

				// Save status
			    AppDelegate.SiriAuthorizationStatus = status;
			});

			base.ViewDidLoad();
        }
	}
}
