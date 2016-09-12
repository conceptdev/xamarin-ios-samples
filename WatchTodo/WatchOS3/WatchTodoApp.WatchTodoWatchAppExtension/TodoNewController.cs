using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.IO;
using SQLite;

namespace WatchTodoExtension
{
	partial class TodoNewController : WatchKit.WKInterfaceController
	{
		bool entryMode = true;
		string enteredText = "";

		public TodoNewController (IntPtr handle) : base (handle)
		{
		}

		partial void WKInterfaceButton5_Activated (WatchKit.WKInterfaceButton sender)
		{
			if (entryMode) {
				Console.WriteLine("EntryMode=true");

				// Straight to dictation (no suggestions)
//				PresentTextInputController(new string[0], WatchKit.WKTextInputMode.Plain, (a) => {
//					Console.WriteLine("Dictation was accepted");
//					if (a != null && a.Count > 0) {
//						enteredText = a.GetItem<NSObject>(0).ToString();
//						Console.WriteLine("   " + a.GetItem<NSObject>(0));
//						Name.SetText (a.GetItem<NSObject>(0).ToString());
//						entryMode = false;
//						EnterText.SetTitle ("Save");
//					} else {
//						Console.WriteLine("Error");
//					}
//				});

				var suggest = new string[]{"Get groceries", "Buy gas", "Post letter"};
				PresentTextInputController( suggest, WatchKit.WKTextInputMode.AllowEmoji, (a) => {
					Console.WriteLine("Text was selected");
					if (a != null && a.Count > 0) {
						enteredText = a.GetItem<NSObject>(0).ToString();
						Console.WriteLine("   " + a.GetItem<NSObject>(0));
						Name.SetText (a.GetItem<NSObject>(0).ToString());
						entryMode = false;
						EnterText.SetTitle ("Save");
					} else {
						Console.WriteLine("Error");
					}

				});
			} else {
				Console.WriteLine("EntryMode=false  must save " + enteredText + ".");
				// TODO: save!
				TodoItemDatabase Database = null;
				var todo = new TodoItem {Name = enteredText, Done = false};

				if (Database == null) 
				{
					//HACK: no app group
					var appGroupContainerPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					Console.WriteLine("agcpath: " + appGroupContainerPath);

					var sqliteFilename = "TodoSQLite.db3";
					var path = Path.Combine(appGroupContainerPath, sqliteFilename);
					var conn = new SQLiteConnection(path);

					Database = new TodoItemDatabase(conn);
				}
				Database.SaveItem(todo);
				Database.Close();

				PopController();
			}
		}
	}
}
