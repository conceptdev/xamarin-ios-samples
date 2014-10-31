using System;
using System.IO;
using System.Text;

namespace Todo
{
	public class TodoItem
	{
		public TodoItem ()
		{
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}

