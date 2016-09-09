using System;
//using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace WatchTodoExtension
{
	public class TodoItemDatabase 
	{
		static object locker = new object ();

		//SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public TodoItemDatabase(object conn) //SQLiteConnection conn)
		{
			//database = conn;

			//database.CreateTable<TodoItem>();
		}

		public void Close()
		{
			//database.Close ();
		}

		public IEnumerable<TodoItem> GetItems ()
		{
			lock (locker) {
				return null;//(from i in database.Table<TodoItem>() select i).ToList();
			}
		}

		public IEnumerable<TodoItem> GetItemsNotDone ()
		{
			lock (locker) {
				return null;//database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
			}
		}

		public TodoItem GetItem (int id) 
		{
			lock (locker) {
				return null;//database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int SaveItem (TodoItem item) 
		{
			lock (locker) {
				//if (item.ID != 0) {
				//	database.Update(item);
				//	return item.ID;
				//} else {
				//	return database.Insert(item);
				//}
				return -1;
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker) {
				return -1;//database.Delete<TodoItem>(id);
			}
		}
	}
}

