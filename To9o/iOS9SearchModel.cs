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
	public class iOS9SearchModel
	{
		//readonly Dictionary<Guid, string> searchIndexMap;
		readonly Dictionary<string, Task> searchIndexMap2;

		/// <returns>Restaurant Name</returns>
		public string Lookup (string g) {
			var r = from s in searchIndexMap2
			        where s.Key == g
			        select s.Value;
			try {
				return r.FirstOrDefault ().Name;
			} catch {
				Console.WriteLine ($"uid {g} not found");
				return "";
			}
		}
		public static void Index (Task t) {
			var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
			attributeSet.Title = t.Name;
			attributeSet.ContentDescription = t.Notes;
			attributeSet.TextContent = t.Notes;

			var dataItem = new CSSearchableItem (t.Id.ToString(), "com.conceptdevelopment.to9o", attributeSet);

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
		// HACK: index each task as it is entered or updated
		public iOS9SearchModel (List<Task> tasks)
		{
			searchIndexMap2 = new Dictionary<string, Task> ();
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

				var dataItem = new CSSearchableItem (t.Id.ToString(), "com.conceptdevelopment.to9o", attributeSet);
				return dataItem;

			});

			// HACK: index should be 'managed' rather than deleted/created each time
			//CSSearchableIndex.DefaultSearchableIndex.DeleteAll(null);

			CSSearchableIndex.DefaultSearchableIndex.Index (dataItems.ToArray<CSSearchableItem> (), err => {
				if (err != null) {
					Console.WriteLine (err);
				} else {
					Console.WriteLine ("Indexed items successfully");
				}
			});
		}



		const string activityType = "com.conceptdevelopment.to9o.detail";
		//
		// https://developer.apple.com/library/prerelease/ios/documentation/General/Conceptual/AppSearch/Activities.html#//apple_ref/doc/uid/TP40016308-CH6-SW1
		// HACK: not working yet... ?
		public static NSUserActivity CreateNSUserActivity(Task userInfo)
		{
			var activity = new NSUserActivity(activityType);
			activity.EligibleForSearch = true;
			activity.EligibleForPublicIndexing = false;
			activity.EligibleForHandoff = false;

			activity.Title = "To9o View Detail !!";
			activity.UserInfo = NSDictionary.FromObjectAndKey (new NSString (userInfo.Id.ToString()), new NSString ("id"));
//			activity.AddUserInfoEntries(NSDictionary.FromObjectAndKey(new NSString("Add Empty Task"), new NSString("Name")));
//			var keywords = new NSString[] {new NSString("Add"), new NSString("Todo"), new NSString("Empty"), new NSString("Task") };
//			activity.Keywords = new NSSet<NSString>(keywords);

			// Attributes
			var nsd = new NSMutableDictionary();
			nsd.Add(new NSString("en"),new NSString("Add Todo"));
			nsd.Add(new NSString("fr"),new NSString("ajouter todo"));
			nsd.Add(new NSString("ja"),new NSString("TODOを追加"));
			nsd.Add(new NSString("es"),new NSString("añadir todo"));
			nsd.Add(new NSString("pt"),new NSString("adicionar todo"));
			nsd.Add(new NSString("ar"),new NSString("إضافة تودو"));
			nsd.Add(new NSString("he"),new NSString("להוסיף todo"));
			var csls = new CSLocalizedString (nsd);

			var attributeSet = new CoreSpotlight.CSSearchableItemAttributeSet ();//"com.conceptdevelopment.to9o.detail");

			if (userInfo.Id <= 0) {
				attributeSet.ContentDescription = "New Item!!";
				attributeSet.DisplayName = csls ;
			} else {
				attributeSet.DisplayName = userInfo.Name + "!!";
				attributeSet.ContentDescription = userInfo.Notes + "!!";
			}
			activity.ContentAttributeSet = attributeSet;

			activity.BecomeCurrent ();

			return activity;

		}
	}
}

