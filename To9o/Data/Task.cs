using System;
using SQLite;

namespace StoryboardTables {
	/// <summary>
	/// Todo item
	/// </summary>
	public class Task : IBusinessEntity{
		public Task ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
		// ContactsUI
		public string For { get; set; }
		// CollectionView sorting
		public int Order { get; set; }
	}
}

