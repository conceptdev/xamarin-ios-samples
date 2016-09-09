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
    [Register ("WatchTodoItemViewController")]
    partial class WatchTodoItemViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Done { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField Name { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField Notes { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Save { get; set; }

        [Action ("Delete_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Delete_TouchUpInside (UIKit.UIButton sender);

        [Action ("Save_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Save_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (DeleteButton != null) {
                DeleteButton.Dispose ();
                DeleteButton = null;
            }

            if (Done != null) {
                Done.Dispose ();
                Done = null;
            }

            if (Name != null) {
                Name.Dispose ();
                Name = null;
            }

            if (Notes != null) {
                Notes.Dispose ();
                Notes = null;
            }

            if (Save != null) {
                Save.Dispose ();
                Save = null;
            }
        }
    }
}