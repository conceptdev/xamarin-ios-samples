using System;
using System.Collections.Generic;
using System.Linq;
using CoreSpotlight;
using MobileCoreServices;

namespace StoryboardTables
{
	/// <summary>
	/// courtesy of Larry O'Brien
	/// </summary>
	public class iOS9SearchModel
	{
		//readonly Dictionary<Guid, string> searchIndexMap;
		readonly Dictionary<Guid, Task> searchIndexMap2;

		/// <returns>Restaurant Name</returns>
		public string Lookup (Guid g) {
			var r = from s in searchIndexMap2
			        where s.Key == g
			        select s.Value;
			try {
				return r.FirstOrDefault ().Name;
			} catch {
				Console.WriteLine ($"GUID {g} not found");
				return "";
			}
		}

		// HACK: index each task as it is entered or updated
		public iOS9SearchModel (List<Task> tasks)
		{
			searchIndexMap2 = new Dictionary<Guid, Task> ();
			foreach (var r in tasks) {
				searchIndexMap2.Add (Guid.NewGuid (), r);
			}
			var dataItems = searchIndexMap2.Select (keyValuePair => {
				var guid = keyValuePair.Key;
				var restaurant = keyValuePair.Value;

				var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
				attributeSet.Title = restaurant.Name;
				attributeSet.ContentDescription = restaurant.Name;
				attributeSet.TextContent = restaurant.Notes;

				var dataItem = new CSSearchableItem (guid.ToString (), "com.conceptdevelopment.to9o", attributeSet);
				return dataItem;

			});

			// HACK: index should be 'managed' rather than deleted/created each time
			CSSearchableIndex.DefaultSearchableIndex.DeleteAll(null);

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

