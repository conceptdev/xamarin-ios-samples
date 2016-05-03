using System;
using System.Collections.Generic;
using System.IO;
using SQLite;


namespace StoryboardTables {
	public class TodoRepository {
		TodoDatabase db = null;
		protected static string dbLocation;		

        public TodoRepository(SQLiteConnection conn, string dbLocation)
		{
			// instantiate the database	
			db = new TodoDatabase(conn, dbLocation);
		}


		public TodoItem GetTodo(int id)
		{
            return db.GetItem<TodoItem>(id);
		}
		
		public IEnumerable<TodoItem> GetTodos ()
		{
			return db.GetItems<TodoItem>();
		}

		public IEnumerable<TodoItem> GetOrderedTodos ()
		{
			return db.GetOrderedItems<TodoItem>();
		}

		public int SaveTodo (TodoItem item)
		{
			return db.SaveItem<TodoItem>(item);
		}

		public int DeleteTodo(int id)
		{
			return db.DeleteItem<TodoItem>(id);
		}
		/// <summary>
		/// Brute-force Order column update
		/// </summary>
		/// <param name="orderedTodos">Ordered todo items.</param>
		public void Reorder (List<TodoItem> orderedTodos){
			var i = 0;
			foreach (var t in orderedTodos) {
				db.UpdateOrder (t, ++i);
			}
		}
		[Obsolete("not used")]
		public void Reorder (int oldOrder, int newOrder) {
			db.Reorder<TodoItem> (oldOrder, newOrder);
		}
	}
}

