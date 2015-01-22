using System;
using System.Drawing;

using Foundation;
using UIKit;
using Xamarin.Auth;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.IO;


namespace WatchInsta
{
	public partial class WatchInstaViewController : UIViewController
	{
		public WatchInstaViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		Account acct;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);


			var FileManager = new NSFileManager ();
			var appGroupContainer = FileManager.GetContainerUrl ("group.com.conceptdevelopment.watchinsta");
			var appGroupContainerPath = appGroupContainer.Path;
			Console.WriteLine ("agcpath: " + appGroupContainerPath);


			IEnumerable<Account> accounts = AccountStore.Create ().FindAccountsForService ("Instagram");
			string token = "";


			acct = (from a in accounts
			        select a).FirstOrDefault ();

			if (acct != null) {

				token = acct.Properties ["access_token"];

				var request = new OAuth2Request (
					"GET", 
					new Uri ("https://api.instagram.com/v1/users/self/feed?access_token=" + token)
					, null
					, acct);//eventArgs.Account);


				request.GetResponseAsync ().ContinueWith (t => {
					if (t.IsFaulted)
						Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
					else {
						string json = t.Result.GetResponseText ();
						Console.WriteLine (json);

						var root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject> (json);

						var webClient = new WebClient();

						var count = Math.Min(5, root.data.Count); var i = 0;

						var inst = root.data[i];
						var thumb = inst.images.thumbnail;
						var url = new Uri(thumb.url);
						Console.WriteLine(inst.caption.text);
						Console.WriteLine("download: " + inst.images.thumbnail.url);

						webClient.DownloadDataCompleted += (s, e) => {
							Console.WriteLine("download completed");
							if (e.Error == null) {
								var bytes = e.Result; // get the downloaded data

								string documentsPath = appGroupContainerPath; //Environment.GetFolderPath(Environment.SpecialFolder.Personal);

								string localFilename = "insta-"+DateTime.Now.Ticks+".png";
								string localPath = Path.Combine (documentsPath, localFilename);
								File.WriteAllBytes (localPath, bytes); // writes to local storage   
								Console.WriteLine ("saved to: " + localPath);
								InvokeOnMainThread (() => {
									instagramImageView.Image = UIImage.FromFile(localPath);
								});
							} else {
								Console.WriteLine(e.Error);
							}


							// other loops
							if (i < count) {
								 inst = root.data[i];
								 thumb = inst.images.thumbnail;
								 url = new Uri(thumb.url);
								Console.WriteLine(inst.caption.text);
								Console.WriteLine(inst.images.thumbnail.url);

								webClient.DownloadDataAsync(url);

								i++;
							}
						};

						// first loop
						webClient.DownloadDataAsync(url);




					}
				});

			
			} else {

				//
				//
				// http://instagram.com/developer/clients/manage/
				//
				var auth = new OAuth2Authenticator (
					          clientId: "YOU_NEED_YOUR_INSTAGRAM_DEV_CLIENTID_HERE",
					          scope: "basic",
					          authorizeUrl: new Uri ("https://api.instagram.com/oauth/authorize/"),
					          redirectUrl: new Uri ("http://your-redirect-url.net/"));

				auth.Completed += (sender, eventArgs) => {
					// We presented the UI, so it's up to us to dimiss it on iOS.
					DismissViewController (true, null);

					if (eventArgs.IsAuthenticated) {
						// Use eventArgs.Account to do wonderful things
						acct = eventArgs.Account;
						AccountStore.Create ().Save (eventArgs.Account, "Instagram");



						token = eventArgs.Account.Properties ["access_token"];






						var request = new OAuth2Request ("GET", 
							             new Uri ("https://api.instagram.com/v1/users/self/feed?access_token=" + token)
						, null, acct);//eventArgs.Account);
						request.GetResponseAsync ().ContinueWith (t => {
							if (t.IsFaulted)
								Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
							else {
								string json = t.Result.GetResponseText ();
								Console.WriteLine (json);
							}
						});
					} else {
						// The user cancelled

					}
				};
				PresentViewController (auth.GetUI (), true, null);
			}





		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

