// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WatchTodo
{
	[Register ("WatchTodoViewController")]
	partial class WatchTodoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem AddButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView todoItemTableView { get; set; }

		[Action ("UIBarButtonItem61_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIBarButtonItem61_Activated (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
			if (todoItemTableView != null) {
				todoItemTableView.Dispose ();
				todoItemTableView = null;
			}
		}
	}
}
