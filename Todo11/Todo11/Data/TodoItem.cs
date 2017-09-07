using System;
using SQLite;

namespace Todo11App {
	/// <summary>
	/// Todo item
	/// </summary>
	public class TodoItem : IBusinessEntity{
		public TodoItem ()
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

