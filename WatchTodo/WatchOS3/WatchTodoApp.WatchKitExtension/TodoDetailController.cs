using System;
using WatchKit;
using Foundation;
using System.IO;
using SQLite;

namespace WatchTodoExtension
{
	public partial class TodoDetailController : WKInterfaceController
	{
		public TodoDetailController (IntPtr handle) : base (handle)
		{
		}

		NSTodoItem todo;

		public override void Awake (NSObject context)
		{
			base.Awake (context);
			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			todo = context as NSTodoItem;
		}
		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);

			Name.SetText (todo.Name);
			Notes.SetText (todo.Notes);
			Done.SetOn (todo.Done);
		}
			
		public TodoItemDatabase Database { get; set; }
		//partial void DoneSwitch (bool value)
		partial void DoneSwitch (WatchKit.WKInterfaceSwitch sender)
		{
			var x = (NSNumber)(NSObject)sender;
			todo.Done = x.BoolValue;
			Console.WriteLine("DoneSwitch : " + sender);
		}
		partial void Save ()
		{
			if (Database == null) 
			{
				var appGroupContainerPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments); 
				Console.WriteLine ("agcpath: " + appGroupContainerPath);

				var sqliteFilename = "TodoSQLite.db3";
				var path = Path.Combine(appGroupContainerPath, sqliteFilename);
				var conn = new SQLiteConnection (path);

				Database = new TodoItemDatabase(conn);
			}
			Console.WriteLine ("Save the todo " + todo.Name);
			Database.SaveItem(todo.As());

			PopController();
		}
		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}

