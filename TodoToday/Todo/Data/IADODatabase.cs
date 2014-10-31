using System;
using System.Collections.Generic;

namespace Todo
{
	public interface IADODatabase
	{
		IEnumerable<TodoItem> GetItems();

		TodoItem GetItem(int id);

		int SaveItem(TodoItem item);

		int DeleteItem(int id);

		IEnumerable<TodoItem> GetItemsNotDone();
	}
}

