using System;

using WatchKit;
using Foundation;
using System.Collections.Generic;
using System.IO;
using SQLite;
using System.Linq;
using WormHoleSharp;

namespace WatchTodoExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		Wormhole wormHole;
		public InterfaceController (IntPtr handle) : base (handle)
		{
			wormHole = new Wormhole ("group.com.conceptdevelopment.WatchTodo", "messageDir");

		}

		List<TodoItem> data = new List<TodoItem>();
		public TodoItemDatabase Database { get; set; }

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			var FileManager = new NSFileManager ();
			var appGroupContainer = FileManager.GetContainerUrl ("group.com.conceptdevelopment.WatchTodo");
			var appGroupContainerPath = appGroupContainer.Path;
			Console.WriteLine ("agcpath: " + appGroupContainerPath);


			var sqliteFilename = "TodoSQLite.db3";
			var path = Path.Combine(appGroupContainerPath, sqliteFilename);
			var conn = new SQLiteConnection (path);

			Database = new TodoItemDatabase(conn);
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

			NSUserDefaults shared = new NSUserDefaults(
				"group.com.conceptdevelopment.WatchTodo", NSUserDefaultsType.SuiteName);
			var isEnabled = shared.BoolForKey ("enabled_preference");
			var name = shared.StringForKey ("name_preference");
			Console.WriteLine ("Enabled: " + isEnabled);
			Console.WriteLine ("Name: " + name);

			// reload each view
			data = Database.GetItems ().ToList();

			TodoTable.SetNumberOfRows ((nint)data.Count, "todoRow");

			for (var i = 0; i < data.Count; i++) {

				var elementRow = (TodoRowController)TodoTable.GetRowController (i);

				elementRow.Set(data [i].Name, data [i].Done);

				Console.WriteLine (i + " set " + data [i].Name + " (" + data[i].Done + ")");
			}

			wormHole.ListenForMessage<string> (WormholeMessage.MessageType, (message) => {
				//SelectionLabel.SetText(message.Id.ToString());
				// reload each view
				var data1 = Database.GetItems ().ToList();

				TodoTable.SetNumberOfRows ((nint)data1.Count, "todoRow");

				for (var i = 0; i < data1.Count; i++) {

					var elementRow = (TodoRowController)TodoTable.GetRowController (i);

					elementRow.Set(data1 [i].Name, data1 [i].Done);

					Console.WriteLine (i + " set " + data1 [i].Name + " (" + data[i].Done + ")");
				}
			});
		}

		public override void DidDeactivate ()
		{
			wormHole.StopListeningForMessageWithIdentifier (WormholeMessage.MessageType);
				
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}

		partial void New()
		{
			PushController ("todoAdd", null);
		}
	}
}

