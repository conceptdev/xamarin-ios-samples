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