using System;
using SQLite;

namespace StoryboardTables {
	/// <summary>
	/// Represents a Task.
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
	}
}

