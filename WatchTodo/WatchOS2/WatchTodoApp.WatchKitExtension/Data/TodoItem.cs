using System;
//using SQLite.Net.Attributes;
using Foundation;
using SQLite;

namespace WatchTodoApp.WatchKitExtension
{
	public class NSTodoItem : NSObject
	{
		public NSTodoItem ()
		{
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }

		public TodoItem As () {
			return new TodoItem {
				ID = this.ID,
				Name = this.Name,
				Notes = this.Notes,
				Done = this.Done
			};
		}
	}

	public class TodoItem
	{
		public TodoItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }

		public NSTodoItem As () {
			return new NSTodoItem {
				ID = this.ID,
				Name = this.Name,
				Notes = this.Notes,
				Done = this.Done
			};
		}
	}
}