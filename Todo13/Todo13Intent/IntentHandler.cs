using System;
using System.Collections.Generic;

using Foundation;
using Intents;

namespace Todo11Intent
{
	// As an example, this class is set up to handle Message intents.
	// You will want to replace this or add other intents as appropriate.
	// The intents you wish to handle must be declared in the extension's Info.plist.

	// You can test your example integration by saying things to Siri like:
	// "Send a message using <myApp>"
	// "<myApp> John saying hello"
	// "Search for messages in <myApp>"
	[Register("IntentHandler")]
	public partial class IntentHandler : INExtension
	{
		protected IntentHandler(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override NSObject GetHandler(INIntent intent)
		{
			// This is the default implementation.  If you want different objects to handle different intents,
			// you can override this and return the handler you want for that particular intent.
			Console.WriteLine("get the intent handler");

            // otherwise, we always return "this" one
			return this;
		}
    }
}
