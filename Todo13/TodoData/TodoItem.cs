using System;
using SQLite;

namespace Todo11App 
{
	/// <summary>
	/// Todo item
	/// </summary>
	public class TodoItem : IBusinessEntity
    {
		public TodoItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
		// Sorting
		public int Order { get; set; }
        // Location
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // Image - filename will be $"{Id}.jpg" (only one image supported per item)
        public bool HasImage { get; set; }
	}
}

