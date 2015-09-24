using System;
using Foundation;
using CoreSpotlight;

namespace StoryboardTables
{
	public class UserActivityHelper
	{
		//
		// https://developer.apple.com/library/prerelease/ios/documentation/General/Conceptual/AppSearch/Activities.html#//apple_ref/doc/uid/TP40016308-CH6-SW1
		// 
		public static NSUserActivity CreateNSUserActivity(Task userInfo)
		{
			var activityType = ActivityTypes.Detail;
			if (userInfo.Id <= 0) {
				activityType = ActivityTypes.Add;
			}
			var activity = new NSUserActivity(activityType);
			activity.EligibleForSearch = true;
			activity.EligibleForPublicIndexing = false;
			activity.EligibleForHandoff = false;

			activity.Title = NSBundle.MainBundle.LocalizedString ("Todo Detail", "");

			//			var keywords = new NSString[] {new NSString("Add"), new NSString("Todo"), new NSString("Empty"), new NSString("Task") };
			//			activity.Keywords = new NSSet<NSString>(keywords);

			// Attributes
			var nsd = new NSMutableDictionary();
			nsd.Add(new NSString("en"),new NSString("Add todo"));
			nsd.Add(new NSString("fr"),new NSString("ajouter todo"));
			nsd.Add(new NSString("ja"),new NSString("TODOを追加"));
			nsd.Add(new NSString("es"),new NSString("añadir todo"));
			nsd.Add(new NSString("pt"),new NSString("adicionar todo"));
			nsd.Add(new NSString("ar"),new NSString("إضافة تودو"));
			nsd.Add(new NSString("he"),new NSString("להוסיף todo"));
			var csls = new CSLocalizedString (nsd);

			var attributeSet = new CoreSpotlight.CSSearchableItemAttributeSet ();

			if (userInfo.Id <= 0) {
				attributeSet.DisplayName = csls ;
				attributeSet.ContentDescription = NSBundle.MainBundle.LocalizedString ("(new)","");
				activity.AddUserInfoEntries(NSDictionary.FromObjectAndKey(new NSString("0"), ActivityKeys.Id));
			} else {
				attributeSet.DisplayName = NSBundle.MainBundle.LocalizedString ("Edit Todo","");
				attributeSet.ContentDescription = userInfo.Name + "!";
				activity.AddUserInfoEntries(NSDictionary.FromObjectAndKey(new NSString(userInfo.Id.ToString()), ActivityKeys.Id));
			}
			activity.ContentAttributeSet = attributeSet;

			activity.BecomeCurrent (); // don't forget to ResignCurrent()

			return activity;
		}
	}
}

