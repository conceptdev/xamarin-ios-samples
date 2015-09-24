using System;
using System.Collections.Generic;
using System.Linq;
using CoreSpotlight;
using MobileCoreServices;
using Foundation;

namespace StoryboardTables
{
	/// <summary>
	/// courtesy of Larry O'Brien
	/// </summary>
	public class SpotlightHelper
	{
		public static void Index (Task t) {
			var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
			attributeSet.Title = t.Name;
			attributeSet.ContentDescription = t.Notes;
			attributeSet.TextContent = t.Notes;

			var dataItem = new CSSearchableItem (t.Id.ToString(), Spotlight.SearchDomain, attributeSet);

			CSSearchableIndex.DefaultSearchableIndex.Index (new CSSearchableItem[] {dataItem}, err => {
				if (err != null) {
					Console.WriteLine (err);
				} else {
					Console.WriteLine ("Indexed inividual item successfully");
				}
			});
		}
		public static void Delete (Task t) {
			CSSearchableIndex.DefaultSearchableIndex.Delete (new string[] {t.Id.ToString()}, err => {
				if (err != null) {
					Console.WriteLine (err);
				} else {
					Console.WriteLine ("Deleted inividual item from CS index");
				}
			});
		}


		[Obsolete("Sample indexes content as created or deleted; but this method could be used to bulk-index")]
		public static void BulkIndex (List<Task> tasks)
		{
			var searchIndexMap2 = new Dictionary<string, Task> ();
			foreach (var r in tasks) {
				searchIndexMap2.Add (Guid.NewGuid ().ToString(), r);
			}
			var dataItems = searchIndexMap2.Select (keyValuePair => {
				var guid = keyValuePair.Key;
				var t = keyValuePair.Value;

				var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
				attributeSet.Title = t.Name;
				attributeSet.ContentDescription = t.Notes;
				attributeSet.TextContent = t.Notes;

				var dataItem = new CSSearchableItem (t.Id.ToString(), Spotlight.SearchDomain, attributeSet);
				return dataItem;

			});

			// Delete everything before doing bulk index
			CSSearchableIndex.DefaultSearchableIndex.DeleteAll(null);

			CSSearchableIndex.DefaultSearchableIndex.Index (dataItems.ToArray<CSSearchableItem> (), err => {
				if (err != null) {
					Console.WriteLine (err);
				} else {
					Console.WriteLine ("Indexed items successfully");
				}
			});
		}

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

			activity.BecomeCurrent ();

			return activity;

		}
	}
}

