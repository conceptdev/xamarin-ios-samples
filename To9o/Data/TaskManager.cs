using System;
using System.Collections.Generic;
using SQLite;

namespace StoryboardTables
{
	public class TaskManager
	{
        TaskRepository repository;

		public TaskManager (SQLiteConnection conn) 
        {
            repository = new TaskRepository(conn, "");
        }

		public Task GetTask(int id)
		{
            return repository.GetTask(id);
		}
		
		public IList<Task> GetTasks ()
		{
            return new List<Task>(repository.GetTasks());
		}
		
		public int SaveTask (Task item)
		{
            return repository.SaveTask(item);
		}
		
		public int DeleteTask(int id)
		{
            return repository.DeleteTask(id);
		}
		
	}
}