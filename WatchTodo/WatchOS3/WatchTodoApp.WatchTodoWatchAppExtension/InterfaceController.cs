using System;

using WatchKit;
using Foundation;
using System.Collections.Generic;
using System.IO;
//using SQLite;
using System.Linq;

namespace WatchTodoExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
//		Wormhole wormHole;
		public InterfaceController (IntPtr handle) : base (handle)
		{
//			wormHole = new Wormhole ("group.co.conceptdev.WatchTodo", "messageDir");

		}

		List<TodoItem> data = new List<TodoItem>();
		public TodoItemDatabase Database { get; set; }

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			//HACK: no app group
			var appGroupContainerPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments); 
			Console.WriteLine ("agcpath: " + appGroupContainerPath);

			var sqliteFilename = "TodoSQLite.db3";
			var path = Path.Combine(appGroupContainerPath, sqliteFilename);
			//var conn = new SQLiteConnection (path);

			//Database = new TodoItemDatabase(conn);

			//if (Database.GetItems ().Count() == 0) {
			//	Database.SaveItem (new TodoItem { Name = "Buy Pineapple" });
			//	Database.SaveItem (new TodoItem { Name = "Buy Plum", Done = true });
			//	Database.SaveItem (new TodoItem { Name = "Buy Kiwi" });
			//	Database.SaveItem (new TodoItem { Name = "Buy Apple", Notes="iPhone6s" });
			//}

			data.Add(new TodoItem { Name = "Buy Pineapple" });
			data.Add(new TodoItem { Name = "Buy Plum", Done = true });
			data.Add(new TodoItem { Name = "Buy Kiwi" });
			data.Add(new TodoItem { Name = "Buy Apple", Notes = "iPhone6s" });
		}

		public override NSObject GetContextForSegue (string segueIdentifier, WKInterfaceTable table, nint rowIndex)
		{
			if (segueIdentifier == "showTodoItem") {
				var nst = data [(int)rowIndex];
				return nst.As();
			}
			return null;
		}


		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);

			NSUserDefaults shared = new NSUserDefaults ();
			var isEnabled = shared.BoolForKey ("enabled_preference");
			var name = shared.StringForKey ("name_preference");
			Console.WriteLine ("Enabled: " + isEnabled);
			Console.WriteLine ("Name: " + name);

			// reload each view
//			data = Database.GetItems ().ToList();


			// HACK: the recommendation is to use Insert and Remove rows
			// becaues if you reload the entire table, all the data is
			// re-sent from the extension to the watch
			TodoTable.SetNumberOfRows ((nint)data.Count, "todoRow");
			for (var i = 0; i < data.Count; i++) {
				var elementRow = (TodoRowController)TodoTable.GetRowController (i);
				elementRow.Set(data [i].Name, data [i].Done);
			}
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}

		partial void New()
		{
			PushController ("todoAdd", (string)null);
		}
	}
}
