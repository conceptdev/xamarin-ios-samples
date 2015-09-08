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
		public ComplicationController ()
		{
			Console.WriteLine ("Complication ctor");
		}

		public override void GetCurrentTimelineEntry (CLKComplication complication, Action<CLKComplicationTimelineEntry> handler)
		{
//			var entry = new CLKComplicationTimelineEntry () {
//				Date = new NSDate (),
//				ComplicationTemplate = new CLKComplicationTemplateCircularSmallRingText ()
//			};
//			handler (entry);
//
			CLKComplicationTimelineEntry entry = null;
			if (complication.Family == CLKComplicationFamily.ModularSmall) {
				var textTemplate = new CLKComplicationTemplateModularSmallSimpleText ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("Xamarin1");

				entry = CLKComplicationTimelineEntry.Create (NSDate.Now, textTemplate);

			} else if (complication.Family == CLKComplicationFamily.UtilitarianSmall) {
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

		public override void GetSupportedTimeTravelDirections (CLKComplication complication, Action<CLKComplicationTimeTravelDirections> Handler)
		{
			Console.WriteLine ("GetSupportedTimeTravelDirections");
			Handler (CLKComplicationTimeTravelDirections.None);
//			Handler (CLKComplicationTimeTravelDirections.Forward | CLKComplicationTimeTravelDirections.Backward);
		}

		public override void GetPlaceholderTemplate (CLKComplication complication, Action<CLKComplicationTemplate> handler)
		{
			Console.WriteLine ("GetPlaceholderTemplate");
			CLKComplicationTemplate template = null;
			if (complication.Family == CLKComplicationFamily.ModularSmall) {
				var textTemplate = new CLKComplicationTemplateModularSmallRingText ();
				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("XAM1");
				template = textTemplate;
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

		public override void GetTimelineStartDate (CLKComplication complication, Action<NSDate> handler)
		{
			handler (null);
		}

		public override void GetTimelineEndDate (CLKComplication complication, Action<NSDate> handler)
		{
			handler (null);
		}

		public override void GetPrivacyBehavior (CLKComplication complication, Action<CLKComplicationPrivacyBehavior> handler)
		{
			handler (CLKComplicationPrivacyBehavior.ShowOnLockScreen);
		}

		public override void GetTimelineEntriesBeforeDate (CLKComplication complication, NSDate beforeDate, nuint limit, Action<CLKComplicationTimelineEntry[]> entries)
		{
			entries (null);
		}
		public override void GetTimelineEntriesAfterDate (CLKComplication complication, NSDate afterDate, nuint limit, Action<CLKComplicationTimelineEntry[]> entries)
		{
			entries (null);
		}
		public override void GetNextRequestedUpdateDate (Action<NSDate> handler)
		{
			handler (null);
		}
	}
}


