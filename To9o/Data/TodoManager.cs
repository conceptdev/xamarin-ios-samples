using System;
using System.Collections.Generic;
using SQLite;

namespace To9oApp
{
	public class TodoManager
	{
        TodoRepository repository;

		public TodoManager (SQLiteConnection conn) 
        {
            repository = new TodoRepository(conn, "");
        }

		public TodoItem GetTodo(int id)
		{
            return repository.GetTodo(id);
		}
		
		public IList<TodoItem> GetTodos ()
		{
            return new List<TodoItem>(repository.GetTodos());
		}

		public IList<TodoItem> GetOrderedTodos ()
		{
			return new List<TodoItem>(repository.GetOrderedTodos());
		}

		public int SaveTodo (TodoItem item)
		{
            return repository.SaveTodo(item);
		}
		
		public int DeleteTodo(int id)
		{
            return repository.DeleteTodo(id);
		}

		public void Reorder (List<TodoItem> orderedTodos) {
			repository.Reorder(orderedTodos);
		}
		[Obsolete("not used")]
		public void Reorder (int oldOrder, int newOrder) {
			repository.Reorder (oldOrder, newOrder);
		}
		
	}
}