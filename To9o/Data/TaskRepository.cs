using System;
using System.Collections.Generic;
using System.IO;
using SQLite;


namespace StoryboardTables {
	public class TaskRepository {
		TaskDatabase db = null;
		protected static string dbLocation;		

        public TaskRepository(SQLiteConnection conn, string dbLocation)
		{
			// instantiate the database	
			db = new TaskDatabase(conn, dbLocation);
		}


		public Task GetTask(int id)
		{
            return db.GetItem<Task>(id);
		}
		
		public IEnumerable<Task> GetTasks ()
		{
			return db.GetItems<Task>();
		}

		public IEnumerable<Task> GetOrderedTasks ()
		{
			return db.GetOrderedItems<Task>();
		}

		public int SaveTask (Task item)
		{
			return db.SaveItem<Task>(item);
		}

		public int DeleteTask(int id)
		{
			return db.DeleteItem<Task>(id);
		}

		public void Reorder (int oldOrder, int newOrder) {
			db.Reorder<Task> (oldOrder, newOrder);
		}
	}
}

