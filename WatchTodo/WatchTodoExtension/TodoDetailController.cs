using System;
using WatchKit;
using Foundation;

namespace WatchTodoExtension
{
	public partial class TodoDetailController : WKInterfaceController
	{
		public TodoDetailController
		(IntPtr handle) : base (handle)
		{
		}

		TodoItem todo;

		public override void Awake (NSObject context)
		{
			base.Awake (context);
			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			todo = context as TodoItem;
		}
		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);

			Name.SetText (todo.Name);
			Notes.SetText (todo.Notes);
			Done.SetOn (todo.Done);
		}

		partial void DoneSwitched (Foundation.NSObject value) {
			Console.WriteLine ("value:" + value);
//			var done = value as NSValue;
//			todo.Done = done;

		}
		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}

