using System;
using Foundation;

namespace WatchTodoExtension
{
	public partial class TodoRowController : NSObject
	{
		public TodoRowController ()
		{
		}

		public void Set (string name, bool done) 
		{
			Console.WriteLine ("set: name=" + name + ", done=" + done);

			// I don't even...
			// WTF: http://stackoverflow.com/questions/28031832/how-can-i-reload-the-data-in-a-watchkit-tableview
			Name.SetText (@""); // this fixes the issue I was having :-\
			Name.SetText (name);

			if (done) {
				DoneImage.SetImage ("done");
				Line.SetColor (UIKit.UIColor.Green);
			} else {
				DoneImage.SetImage ("notdone");
				Line.SetColor (UIKit.UIColor.Red);
			}
		}
	}
}