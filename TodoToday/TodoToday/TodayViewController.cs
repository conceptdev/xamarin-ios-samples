using System;
using System.Drawing;

using NotificationCenter;
using Foundation;
using Social;
using UIKit;
using AVFoundation;

namespace TodoToday
{
	public partial class TodayViewController : SLComposeServiceViewController, INCWidgetProviding
	{
		public TodayViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

//		string textToSpeak = "";

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			PlayButton.TouchUpInside += (sender, e) => {
				UIApplication.SharedApplication.OpenUrl (new NSUrl ("todotoday://"));
//				Console.WriteLine("Try to speak: " + textToSpeak);
//				Speak (textToSpeak);
			};

			// Do any additional setup after loading the view.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NSUserDefaults shared = new NSUserDefaults("group.co.conceptdev.TodoToday", NSUserDefaultsType.SuiteName);
			var count = shared.IntForKey ("TodoCount");
			Console.WriteLine ("Read NSUserDefaults TodoCount: " + count);

			if (count == 0) {
				todoLabel.Text = "nothing to do";
			} else if (count == 1) {
				todoLabel.Text = "one thing to do";
			} else  {
				todoLabel.Text = String.Format("{0} things to do", count);
			}

//			textToSpeak = todoLabel.Text;

		}

		//BUG: https://bugzilla.xamarin.com/show_bug.cgi?id=23540
		public void WidgetPerformUpdate (Action<NCUpdateResult> completionHandler)
		{
			// Perform any setup necessary in order to update the view.

			// If an error is encoutered, use NCUpdateResultFailed
			// If there's no update required, use NCUpdateResultNoData
			// If there's an update, use NCUpdateResultNewData

			completionHandler (NCUpdateResult.NewData);
		}

		float volume = 0.5f;
		float pitch = 1.0f;

		void Speak (string text)
		{
			var speechSynthesizer = new AVSpeechSynthesizer ();

			var speechUtterance = new AVSpeechUtterance (text) {
				Rate = AVSpeechUtterance.MaximumSpeechRate/4,
				Voice = AVSpeechSynthesisVoice.FromLanguage ("en-AU"),
				Volume = volume,
				PitchMultiplier = pitch
			};

			speechSynthesizer.SpeakUtterance (speechUtterance);
		}
	}
}

