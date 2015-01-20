using System;

using WatchKit;
using Foundation;
using System.Collections.Generic;

namespace WatchTodoExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		List<TodoItem> data = new List<TodoItem>();

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);


			data.Add (new TodoItem{ Name="buy apples", Done=true});
			data.Add (new TodoItem{ Name="buy bananas"});
			data.Add (new TodoItem{ Name="buy pears", Done=true});
			data.Add (new TodoItem{ Name="buy rockmelon"});
			data.Add (new TodoItem{ Name="buy kiwi"});
			data.Add (new TodoItem{ Name="buy dragonfruit"});
		}

		public override NSObject GetContextForSegue (string segueIdentifier, WKInterfaceTable table, nint rowIndex)
		{
			if (segueIdentifier == "showTodoItem") {
				return data[(int)rowIndex];
			}
			return null;
		}


		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);

			TodoTable.SetNumberOfRows ((nint)data.Count, "todoRow");

			for (var i = 0; i < data.Count; i++) {
				var elementRow = (TodoRowController)TodoTable.GetRowController (i);
				elementRow.Name.SetText (data [i].Name);
				if (data [i].Done)
					elementRow.DoneImage.SetImage ("done");
				else
					elementRow.DoneImage.SetImage ("notdone");
			}
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}

