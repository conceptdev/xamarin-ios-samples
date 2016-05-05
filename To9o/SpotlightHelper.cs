using System;
using System.Collections.Generic;
using System.Linq;
using CoreSpotlight;
using MobileCoreServices;
using Foundation;

namespace To9oApp
{
	/// <summary>
	/// courtesy of Larry O'Brien
	/// </summary>
	public class SpotlightHelper
	{
		public static void Index (TodoItem t) {
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
		public static void Delete (TodoItem t) {
			CSSearchableIndex.DefaultSearchableIndex.Delete (new string[] {t.Id.ToString()}, err => {
				if (err != null) {
					Console.WriteLine (err);

				} else {
					Console.WriteLine ("Deleted inividual item from CS index");
				}
			});
		}


		[Obsolete("This sample indexes content as created or deleted; but this method could be used to bulk-index")]
		public static void BulkIndex (List<TodoItem> todoItems)
		{
			// HACK: generating fake GUID keys
			var searchIndexMap2 = new Dictionary<string, TodoItem> ();
			foreach (var r in todoItems) {
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

