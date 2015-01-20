// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WatchTodoExtension
{
	[Register ("TodoRowController")]
	partial class TodoRowController
	{
		[Outlet]
		public WatchKit.WKInterfaceImage DoneImage { get; private set; }

		[Outlet]
		public WatchKit.WKInterfaceLabel Name { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}

			if (DoneImage != null) {
				DoneImage.Dispose ();
				DoneImage = null;
			}
		}
	}
}
