using System;
using System.Collections.Generic;
using SQLite;

namespace Todo11App
{
    public partial class TodoManager
    {
        public void InsertSampleData ()
        {
            if (GetTodos().Count == 0)
            {
                SaveTodo(new TodoItem { Name = "1" });
                SaveTodo(new TodoItem { Name = "2" });
                SaveTodo(new TodoItem { Name = "3" });
                SaveTodo(new TodoItem { Name = "4" });
                SaveTodo(new TodoItem { Name = "5" });
            }
        }
    }
}