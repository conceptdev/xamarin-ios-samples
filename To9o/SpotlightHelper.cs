using System;
using System.Collections.Generic;
using System.Linq;
using CoreSpotlight;
using MobileCoreServices;
using Foundation;
using Xamarin;

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
					Insights.Report(new Exception("CoreSpotlight Index Failed"), new Dictionary <string, string> { 
						{"Message", err.ToString()}
					}, Xamarin.Insights.Severity.Error);
				} else {
					Console.WriteLine ("Indexed inividual item successfully");
					Insights.Track("CoreSpotlight", new Dictionary<string, string> {
						{"Type", "Indexed successfully"}
					});
				}
			});
		}
		public static void Delete (Task t) {
			CSSearchableIndex.DefaultSearchableIndex.Delete (new string[] {t.Id.ToString()}, err => {
				if (err != null) {
					Console.WriteLine (err);
					Insights.Report(new Exception("CoreSpotlight Delete Failed"), new Dictionary <string, string> { 
						{"Message", err.ToString()}
					}, Xamarin.Insights.Severity.Error);
				} else {
					Console.WriteLine ("Deleted inividual item from CS index");
				}
			});
		}


		[Obsolete("This sample indexes content as created or deleted; but this method could be used to bulk-index")]
		public static void BulkIndex (List<Task> tasks)
		{
			// HACK: generating fake GUID keys
			var searchIndexMap2 = new Dictionary<string, Task> ();
			foreach (var r in tasks) {
				searchIndexMap2.Add (Guid.NewGuid ().ToString(), r);
			}
			// mapping keys to objects
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

			// Bulk index
			CSSearchableIndex.DefaultSearchableIndex.Index (dataItems.ToArray<CSSearchableItem> (), err => {
				if (err != null) {
					Console.WriteLine (err);
				} else {
					Console.WriteLine ("Indexed items successfully");
				}
			});
		}
	}
}

