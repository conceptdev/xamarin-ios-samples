using System;
using System.Linq;
using System.Collections.Generic;
using SQLite;

namespace Todo11App
{
	/// <summary>
	/// TodoDatabase builds on SQLite.Net and represents a specific database, in our case, the Todo DB.
	/// It contains methods for retrieval and persistance as well as db creation, all based on the 
	/// underlying ORM.
	/// </summary>
	public class TodoDatabase 
	{
		static object locker = new object ();

        SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the Todo Database. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public TodoDatabase(SQLiteConnection conn, string path)
		{
            database = conn;
			// create the tables
			database.CreateTable<TodoItem>();
		}
		
		public IEnumerable<T> GetItems<T> () where T : IBusinessEntity, new ()
		{
			lock (locker) {
                return (from i in database.Table<T>() select i).ToList();
            }
		}

		public T GetItem<T> (int id) where T : IBusinessEntity, new ()
		{
            lock (locker) {
                try
                {
                    var sql = $"SELECT * FROM [TodoItem] WHERE [Id] = ?";
                    return database.Query<T>(sql, id).FirstOrDefault();

                } catch (Exception ex)
                {
                    Console.WriteLine("Data error (primary key?) " + ex.Message);
                    return database.Table<T>().First(); // HACK: return wrong result
                }
            }
		}

		public int SaveItem<T> (T item) where T : IBusinessEntity
		{
            lock (locker) {
                if (item.Id != 0) {
                    database.Update(item);
                    return item.Id;
                } else {
                    return database.Insert(item);
                }
            }
		}
		
		public int DeleteItem<T>(int id) where T : IBusinessEntity, new ()
		{
            lock (locker) {
                return database.Delete<T>(id);
            }
		}

		public IEnumerable<T> GetOrderedItems<T> () where T : IBusinessEntity, new ()
		{
			var sql = $"SELECT * FROM [TodoItem] ORDER BY [Order]";
			lock (locker) {
				return database.Query<T> (sql).ToList ();
			}
		}
		/// <summary>
		/// Row-by-row Order update
		/// </summary>
		public void UpdateOrder<T> (T item, int newOrder) where T : IBusinessEntity, new ()
		{
			var sql1 = $"UPDATE [TodoItem] SET [Order] = ? WHERE [Id] = ?";
			lock (locker) {
				database.Query<T> (sql1, newOrder, item.Id);
			}
			
		}
		//[Obsolete("note used")]
		//public void Reorder<T> (int oldOrder, int newOrder) where T : IBusinessEntity, new ()
		//{
		//	var sql1 = $"UPDATE [TodoItem] SET [Order] = [Order] + 1 WHERE [Order] > ? AND [Order] < ?";
		//	var sql2 = $"UPDATE [TodoItem] SET [Order] = ? WHERE [Order] = ?";
		//	lock (locker) {
		//		database.Query<T> (sql1, oldOrder, newOrder);
		//		database.Query<T> (sql2, newOrder, oldOrder);
		//	}
		//}
	}
}