using System;

using Foundation;
using WatchKit;
using ClockKit;
using UIKit;

/*
https://developer.apple.com/library/prerelease/watchos/documentation/General/Conceptual/AppleWatch2TransitionGuide/DesigningaComplication.html
https://github.com/shu223/watchOS-2-Sampler/blob/master/watchOS2Sampler%20WatchKit%20Extension/ComplicationController.swift

https://www.bignerdranch.com/blog/watchkit-2-complications/

http://code.tutsplus.com/tutorials/an-introduction-to-clockkit--cms-24247
http://www.sneakycrab.com/blog/2015/6/10/writing-your-own-watchkit-complications
*/
namespace WatchComplication
{
	[Register ("ComplicationController")]
	public class ComplicationController : CLKComplicationDataSource
	{
		// when this ctor isn't supplied, "Extension received request to wake up for complication support" never happens :(
		public ComplicationController () 
		{
			Console.WriteLine ("Complication ctor");
		}
		// expected this to be called (because `Register`) but it does not ever get called :(
		public ComplicationController (IntPtr p) : base (p) 
		{
			Console.WriteLine ("Complication ctor IntPtr");
		}
		public override void GetNextRequestedUpdateDate (Action<NSDate> handler)
		{
			Console.WriteLine ("GetNextRequestedUpdateDate");

			var nextUpdateDate = new NSDate ();
			nextUpdateDate.AddSeconds (10);
			handler (nextUpdateDate);
		}
		public override void GetCurrentTimelineEntry (CLKComplication complication, Action<CLKComplicationTimelineEntry> handler)
		{
			Console.WriteLine ("GetCurrentTimelineEntry");

			CLKComplicationTimelineEntry entry = null;

			// default
			var textTemplate1 = new CLKComplicationTemplateModularSmallSimpleText ();
			textTemplate1.TextProvider = CLKSimpleTextProvider.FromText ("Xamarin1");
			entry = CLKComplicationTimelineEntry.Create (NSDate.Now, textTemplate1);

			// may be overwritten
			if (complication.Family == CLKComplicationFamily.ModularLarge) {
				var textTemplate = new CLKComplicationTemplateModularLargeStandardBody ();
				textTemplate.Body1TextProvider = CLKSimpleTextProvider.FromText ("Body1x", "X1", "~~~");
				textTemplate.Body2TextProvider = CLKSimpleTextProvider.FromText ("Body 2x", "X2", "---");
				textTemplate.HeaderTextProvider = CLKSimpleTextProvider.FromText ("HeaderX", "XH", "```");

				entry = CLKComplicationTimelineEntry.Create (NSDate.Now, textTemplate);
			}  else if (complication.Family == CLKComplicationFamily.UtilitarianSmall) {
				var textTemplate = new CLKComplicationTemplateUtilitarianSmallFlat ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("Xamarin2");

				entry = CLKComplicationTimelineEntry.Create (NSDate.Now, textTemplate);
			} else if (complication.Family == CLKComplicationFamily.UtilitarianLarge) {
				var textTemplate = new CLKComplicationTemplateUtilitarianLargeFlat ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("Xamarin3");

				entry = CLKComplicationTimelineEntry.Create (NSDate.Now, textTemplate);
			} else if (complication.Family == CLKComplicationFamily.CircularSmall) {
				var imgTemplate = new CLKComplicationTemplateCircularSmallRingImage ();

				imgTemplate.ImageProvider = CLKImageProvider.Create (UIImage.FromBundle ("Circular"));

				entry = CLKComplicationTimelineEntry.Create (NSDate.Now, imgTemplate);
			}
			else	 
			{
				Console.WriteLine ("GetCurrentTimelineEntry: Complication not supported");
			}

			handler (entry);
		}

		public override void GetPlaceholderTemplate (CLKComplication complication, Action<CLKComplicationTemplate> handler)
		{
			Console.WriteLine ("GetPlaceholderTemplate");

			CLKComplicationTemplate template = null;
			if (complication.Family == CLKComplicationFamily.ModularSmall) {
				var textTemplate = new CLKComplicationTemplateModularSmallRingText ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("XAM1");
				template = textTemplate;
			} else if (complication.Family == CLKComplicationFamily.ModularSmall) {
				var textTemplate = new CLKComplicationTemplateModularLargeStandardBody ();
				textTemplate.HeaderTextProvider = CLKSimpleTextProvider.FromText ("Header A", "A 2", "~~~");
				textTemplate.Body1TextProvider = CLKSimpleTextProvider.FromText ("Body B", "B 2", "~~~");
			} else if (complication.Family == CLKComplicationFamily.UtilitarianSmall) {
				var textTemplate = new CLKComplicationTemplateUtilitarianSmallFlat ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("XAM2");
				template = textTemplate;
			} else if (complication.Family == CLKComplicationFamily.UtilitarianLarge) {
				var textTemplate = new CLKComplicationTemplateUtilitarianLargeFlat ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("XAM3");
				template = textTemplate;
			} else if (complication.Family == CLKComplicationFamily.CircularSmall) {
				var textTemplate = new CLKComplicationTemplateCircularSmallRingText ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("XS");
				template = textTemplate;
			}
			else	 
			{
				Console.WriteLine ("GetPlaceholderTemplate: Complication not supported");
			}

			handler (template);
		}


		public override void GetSupportedTimeTravelDirections (CLKComplication complication, Action<CLKComplicationTimeTravelDirections> Handler)
		{
			Console.WriteLine ("GetSupportedTimeTravelDirections");
			Handler (CLKComplicationTimeTravelDirections.None);
		}

		public override void GetTimelineStartDate (CLKComplication complication, Action<NSDate> handler)
		{
			Console.WriteLine ("GetTimelineStartDate");
			handler (null);
		}

		public override void GetTimelineEndDate (CLKComplication complication, Action<NSDate> handler)
		{
			Console.WriteLine ("GetTimelineEndDate");
			handler (null);
		}

		public override void GetPrivacyBehavior (CLKComplication complication, Action<CLKComplicationPrivacyBehavior> handler)
		{
			Console.WriteLine ("GetPrivacyBehavior");
			handler (CLKComplicationPrivacyBehavior.ShowOnLockScreen);
		}

		public override void GetTimelineEntriesBeforeDate (CLKComplication complication, NSDate beforeDate, nuint limit, Action<CLKComplicationTimelineEntry[]> entries)
		{
			Console.WriteLine ("GetTimelineEntriesBeforeDate");
			entries (null);
		}
		public override void GetTimelineEntriesAfterDate (CLKComplication complication, NSDate afterDate, nuint limit, Action<CLKComplicationTimelineEntry[]> entries)
		{
			Console.WriteLine ("GetTimelineEntriesAfterDate");
			entries (null);
		}
	}
}