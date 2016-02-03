//using System;

//using Foundation;
//using WatchKit;
//using ClockKit;
//using UIKit;

//namespace WatchComplication
//{
//	[Register ("ComplicationController")]
//	public class ComplicationController : CLKComplicationDataSource
//	{
//		// when this ctor isn't supplied, "Extension received request to wake up for complication support" never happens :(
//		public ComplicationController () 
//		{
//			Console.WriteLine ("School Complication ctor");
//		}
//		// expected this to be called (because `Register`) but it does not ever get called :(
//		public ComplicationController (IntPtr p) : base (p) 
//		{
//			Console.WriteLine ("School Complication ctor IntPtr");
//		}
//		public override void GetCurrentTimelineEntry (CLKComplication complication, Action<CLKComplicationTimelineEntry> handler)
//		{
//			Console.WriteLine ("School GetCurrentTimelineEntry for " + DateTime.Now.Hour);

//			CLKComplicationTimelineEntry entry = null;
//			try
//			{
//				var txt = "sleeping";
//				if (DateTime.Now.Hour < 8)
//					txt = "sleeping";
//				else if (DateTime.Now.Hour < 9)
//					txt = "breakfast";
//				else if (DateTime.Now.Hour < 10)
//					txt = "maths";
//				else if (DateTime.Now.Hour < 11)
//					txt = "science";
//				else if (DateTime.Now.Hour < 12)
//					txt = "art";
//				else if (DateTime.Now.Hour < 13)
//					txt = "lunch";
//				else if (DateTime.Now.Hour < 14)
//					txt = "history";
//				else if (DateTime.Now.Hour < 15)
//					txt = "biology";
//				else if (DateTime.Now.Hour < 16)
//					txt = "sport";
//				else if (DateTime.Now.Hour < 17)
//					txt = "study";
//				else if (DateTime.Now.Hour < 19)
//					txt = "dinner";

//				if (complication.Family == CLKComplicationFamily.UtilitarianLarge)
//				{
//					var textTemplate = new CLKComplicationTemplateUtilitarianLargeFlat();
//					textTemplate.TextProvider = CLKSimpleTextProvider.FromText(txt);
//					entry = CLKComplicationTimelineEntry.Create(NSDate.Now, textTemplate);
//				}
//				else
//				{
//					Console.WriteLine("GetCurrentTimelineEntry: Complication not supported (" + complication.Family + ")");
//				}
//			}
//			catch (Exception x)
//			{
//				Console.WriteLine("School Exception " + x);
//			}
//			handler (entry);
//		}

//		public override void GetPlaceholderTemplate (CLKComplication complication, Action<CLKComplicationTemplate> handler)
//		{
//			Console.WriteLine ("School GetPlaceholderTemplate for " + complication);

//			CLKComplicationTemplate template = null;

//			if (complication.Family == CLKComplicationFamily.UtilitarianLarge) {
//				var textTemplate = new CLKComplicationTemplateUtilitarianLargeFlat ();
//				textTemplate.TextProvider = CLKSimpleTextProvider.FromText ("School Schedule");
//				template = textTemplate;

//			} else  
//			{
//				Console.WriteLine ("School GetPlaceholderTemplate: Complication not supported (" + complication + ")");
//			}

//			handler (template);
//		}


//		public override void GetSupportedTimeTravelDirections (CLKComplication complication, Action<CLKComplicationTimeTravelDirections> handler)
//		{
//			Console.WriteLine ("School GetSupportedTimeTravelDirections");
//			handler (CLKComplicationTimeTravelDirections.None);
//		}


//		//public override void GetNextRequestedUpdateDate (Action<NSDate> handler)
//		//{
//		//	Console.WriteLine ("School GetNextRequestedUpdateDate");

//		//	var nextUpdateDate = new NSDate ();
//		//	nextUpdateDate = nextUpdateDate.AddSeconds (60*60);
//		//	handler (nextUpdateDate);
//		//}

//		//public override void GetTimelineStartDate (CLKComplication complication, Action<NSDate> handler)
//		//{
//		//	Console.WriteLine ("GetTimelineStartDate");
//		//	handler (null);
//		//}

//		//public override void GetTimelineEndDate (CLKComplication complication, Action<NSDate> handler)
//		//{
//		//	Console.WriteLine ("GetTimelineEndDate");
//		//	handler (null);
//		//}

//		//public override void GetPrivacyBehavior (CLKComplication complication, Action<CLKComplicationPrivacyBehavior> handler)
//		//{
//		//	Console.WriteLine ("GetPrivacyBehavior");
//		//	handler (CLKComplicationPrivacyBehavior.ShowOnLockScreen);
//		//}

//		//public override void GetTimelineEntriesBeforeDate (CLKComplication complication, NSDate beforeDate, nuint limit, Action<CLKComplicationTimelineEntry[]> entries)
//		//{
//		//	Console.WriteLine ("GetTimelineEntriesBeforeDate");
//		//	entries (null);
//		//}
//		//public override void GetTimelineEntriesAfterDate (CLKComplication complication, NSDate afterDate, nuint limit, Action<CLKComplicationTimelineEntry[]> entries)
//		//{
//		//	Console.WriteLine ("GetTimelineEntriesAfterDate");
//		//	entries (null);
//		//}
//	}
//}