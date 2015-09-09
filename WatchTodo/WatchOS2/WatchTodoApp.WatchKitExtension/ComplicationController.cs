using System;

using Foundation;
using WatchKit;
using ClockKit;
using UIKit;

namespace WatchTodoApp.WatchKitExtension
{
	[Register ("ComplicationController")]
	public class ComplicationController : CLKComplicationDataSource
	{
		public ComplicationController ()
		{
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
			Handler (CLKComplicationTimeTravelDirections.Forward | CLKComplicationTimeTravelDirections.Backward);
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
	}
}


