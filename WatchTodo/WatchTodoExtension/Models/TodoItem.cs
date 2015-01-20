using System;
//using SQLite.Net.Attributes;
using Foundation;

namespace WatchTodoExtension
{
	public class TodoItem : NSObject
	{
		public TodoItem ()
		{
		}

//		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}